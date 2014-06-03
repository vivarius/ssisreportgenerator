using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SSISReportGeneratorTask110.ReportService2005;
using SSISReportGeneratorTask110.ReportService2006;
using CatalogItem = SSISReportGeneratorTask110.ReportService2005.CatalogItem;
using CredentialRetrievalEnum = SSISReportGeneratorTask110.ReportService2005.CredentialRetrievalEnum;
using DataSource = SSISReportGeneratorTask110.ReportService2005.DataSource;
using DataSourceDefinition = SSISReportGeneratorTask110.ReportService2005.DataSourceDefinition;
using DataSourceReference = SSISReportGeneratorTask110.ReportService2005.DataSourceReference;
using ItemTypeEnum = SSISReportGeneratorTask110.ReportService2005.ItemTypeEnum;

namespace SSISReportGeneratorTask110.ReportingHandlers
{
    public class ReportServerProperties : IDisposable
    {
        #region ctor
        protected ReportServerProperties() { }

        public bool IsSharePointIntegratedMode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportServerProperties"/> class.
        /// </summary>
        /// <param name="reportServer">The _ report server.</param>
        public ReportServerProperties(string reportServer)
        {
            ReportServer = reportServer;
        }

        private ReportingService2005 _reportsServerInstance2005;
        private ReportingService2006 _reportsServerInstance2006;

        private CatalogItem[] _returnedItems;
        #endregion

        #region Properties
        public ReportingService2005 ReportsServerInstance2005
        {
            get
            {
                return _reportsServerInstance2005 ?? (_reportsServerInstance2005 = new ReportingService2005
                                                            {
                                                                Credentials = System.Net.CredentialCache.DefaultCredentials,
                                                                Url = ReportServer
                                                            });
            }
        }

        public ReportingService2006 ReportsServerInstance2006
        {
            get
            {
                return _reportsServerInstance2006 ?? (_reportsServerInstance2006 = new ReportingService2006
                {
                    Credentials = System.Net.CredentialCache.DefaultCredentials,
                    Url = ReportServer
                });
            }
        }

        public string Reportname
        {
            get;
            set;
        }

        public string ReportServer
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool WindowsAuthorization
        {
            get;
            set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the data source.
        /// </summary>
        /// <param name="dataSourceName">Name of the _data source.</param>
        /// <param name="dataSourceLocation">The _data source location.</param>
        /// <param name="sqlServerName">Name of the _SQL server.</param>
        /// <param name="dbName">Name of the _DB.</param>
        /// <returns></returns>
        public bool CreateDataSource(string dataSourceName, string dataSourceLocation, string sqlServerName, string dbName)
        {
            bool resVal;

            var dataSourceDefinition = new DataSourceDefinition
                                    {
                                        Extension = "SQL",
                                        ConnectString =
                                            @"Data Source=" + sqlServerName +
                                            @";Initial Catalog=" + dbName,
                                        ImpersonateUserSpecified = true,
                                        Prompt = null,
                                        WindowsCredentials = true,
                                        CredentialRetrieval = CredentialRetrievalEnum.Integrated,
                                        Enabled = true
                                    };
            try
            {
                _reportsServerInstance2005.CreateDataSource(dataSourceName,
                                     dataSourceLocation,
                                     false,
                                     dataSourceDefinition,
                                     null);
                resVal = true;
            }
            catch (Exception)
            {
                resVal = false;
            }

            return resVal;


        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="folderDestinationPath">The _ folder destination path.</param>
        /// <param name="folderName">Name of the _ folder.</param>
        /// <returns></returns>
        public bool CreateFolder(string folderDestinationPath, string folderName)
        {
            bool resVal;

            try
            {
                _reportsServerInstance2005.CreateFolder(folderName, folderDestinationPath.Replace(@"\", "/"), null);
                resVal = true;
            }
            catch (Exception)
            {
                resVal = false;
            }

            return resVal;
        }

        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="sharedDataSourcePath">The shared data source path.</param>
        /// <param name="dataSourceName">Name of the data source.</param>
        /// <returns></returns>
        public DataSource GetDataSource(string sharedDataSourcePath, string dataSourceName)
        {
            var dataSources = ReportsServerInstance2005.GetItemDataSources(sharedDataSourcePath);

            return dataSources.Where(dataSource => dataSource.Name == dataSourceName).FirstOrDefault();
        }

        /// <summary>
        /// Gets the data source definition.
        /// </summary>
        /// <param name="sharedDataSourcePath">The shared data source path.</param>
        /// <returns></returns>
        public DataSourceDefinition GetDataSourceDefinition(string sharedDataSourcePath)
        {
            return ReportsServerInstance2005.GetDataSourceContents(sharedDataSourcePath);
        }

        /// <summary>
        /// Attaches the data source to report.
        /// </summary>
        /// <param name="dataSourceName">Name of the data source.</param>
        /// <param name="dataSourcePath">The data source path.</param>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="reportLocation">The report location.</param>
        /// <returns></returns>
        public bool AttachDataSourceToReport(string dataSourceName, string dataSourcePath, string reportName, string reportLocation)
        {
            bool resVal;

            try
            {
                string fullReportPath = (reportLocation + "/" + reportName).Replace("//", @"/");
                string fullDataSourcePath = (dataSourcePath.Replace(dataSourceName, string.Empty) + dataSourceName).Replace(@"\", @"/");

                DataSource[] dataSources = _reportsServerInstance2005.GetItemDataSources(fullReportPath);

                var dsRef = new DataSourceReference
                                {
                                    Reference = fullDataSourcePath
                                };

                dataSources[0].Item = dsRef;

                _reportsServerInstance2005.SetItemDataSources(fullReportPath, dataSources);

                resVal = true;
            }
            catch (Exception)
            {
                resVal = false;
            }

            return resVal;
        }

        /// <summary>
        /// Checks the item exist.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="path">The path.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns></returns>
        public bool CheckItemExist(ItemTypeEnum type, string path, string folderName)
        {
            var conditions = new SearchCondition[1];
            conditions[0] = new SearchCondition
            {
                Condition = ConditionEnum.Contains,
                ConditionSpecified = true,
                Name = "Name",
                Value = folderName
            };

            _returnedItems = _reportsServerInstance2005.FindItems(path, BooleanOperatorEnum.Or, conditions);

            return (_returnedItems.Where(item => item.Type == type)).Any(item => item.Path == path + "/" + folderName);
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="path">The path.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns></returns>
        public bool DeleteItem(ItemTypeEnum type, string path, string folderName)
        {
            path = path.Substring(path.Length - 1, 1) == @"\"
                       ? path.Substring(0, path.Length - 1).Replace(@"\", @"/")
                       : path.Replace(@"\", @"/");

            bool resVal = false;
            if (CheckItemExist(type, path, folderName))
            {
                _reportsServerInstance2005.DeleteItem(path + "/" + folderName);
                resVal = true;
            }
            return resVal;
        }

        /// <summary>
        /// Deploys the report.
        /// </summary>
        /// <param name="rsSource">Report Server Source object //ReportingService2005</param>
        /// <param name="reportNameSource">Report Source Name</param>
        /// <param name="reportSourcePath">Report Source path</param>
        /// <param name="reportDestinationPath">Report Destination Path</param>
        /// <param name="dataSource">Report Destination DataSource</param>
        /// <param name="dataSourceLocation">Report DataSource Path</param>
        /// <returns>True or False</returns>
        public bool DeployReport(ReportingService2005 rsSource,
                                 string reportNameSource,
                                 string reportSourcePath,
                                 string reportDestinationPath,
                                 string dataSource,
                                 string dataSourceLocation)
        {
            bool resVal;

            try
            {
                if (rsSource == null)
                    return false;

                byte[] reportDefinition = rsSource.GetReportDefinition(reportSourcePath.Replace(@"\", "/"));

                DeleteItem(ItemTypeEnum.Report,
                           reportDestinationPath.Replace(reportNameSource, string.Empty).Replace(@"\", "/"),
                           reportNameSource);

                ReportsServerInstance2005.CreateReport(reportNameSource,
                                                   reportDestinationPath.Replace(reportNameSource, string.Empty).Replace(@"\", "/"),
                                                   true,
                                                   reportDefinition,
                                                   null);
                try
                {
                    AttachDataSourceToReport(dataSource,
                                         dataSourceLocation.Replace(dataSource, string.Empty).Replace(@"\", "/"),
                                         reportNameSource,
                                         reportDestinationPath.Replace(reportNameSource, string.Empty).Replace(@"\", "/"));
                }
                catch (Exception)
                {
                    MessageBox.Show(string.Format("The Report {0} was created but the report's Datasource cannot be updated! Please do it manually... (if you want)", reportNameSource));
                }

                resVal = true;
            }
            catch (Exception)
            {
                resVal = false;
            }


            return resVal;
        }

        /// <summary>
        /// Deploys the report.
        /// </summary>
        /// <param name="rsSource">Report Server Source object //ReportingService2005</param>
        /// <param name="filePath">Report Source Path (local)</param>
        /// <param name="reportDestinationPath">Report Destination Path</param>
        /// <param name="dataSource">Report Destination DataSource</param>
        /// <param name="dataSourceLocation">Report DataSource Path</param>
        /// <returns>True or False</returns>
        public bool DeployReport(ReportingService2005 rsSource,
                                 string filePath,
                                 string reportDestinationPath,
                                 string dataSource,
                                 string dataSourceLocation)
        {
            bool resVal;
            try
            {
                reportDestinationPath = reportDestinationPath.Substring(reportDestinationPath.Length - 1, 1) == @"\"
                           ? reportDestinationPath.Substring(0, reportDestinationPath.Length - 1).Replace(@"\", @"/")
                           : reportDestinationPath.Replace(@"\", @"/");

                if (rsSource == null)
                    return false;

                var fileInfo = new FileInfo(filePath);

                string fileName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

                DeleteItem(ItemTypeEnum.Report,
                           reportDestinationPath.Replace(fileName, string.Empty).Replace(@"\", "/"),
                           fileName);

                ReportsServerInstance2005.CreateReport(fileName,
                                                   reportDestinationPath.Replace(fileName, string.Empty).Replace(@"\", "/"),
                                                   true,
                                                   File.ReadAllBytes(filePath),
                                                   null);
                try
                {
                    AttachDataSourceToReport(dataSource,
                                             dataSourceLocation.Replace(dataSource, string.Empty).Replace(@"\", "/"),
                                             fileName,
                                             reportDestinationPath.Replace(fileName, string.Empty).Replace(@"\", "/"));
                }
                catch (Exception)
                {
                    MessageBox.Show(string.Format("The Report {0} was created but the report's Datasource cannot be updated! Please do it manually... (if you want)", fileName));
                }
                resVal = true;

            }
            catch (Exception)
            {
                resVal = false;
            }

            return resVal;
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (_reportsServerInstance2005 != null)
                _reportsServerInstance2005.Dispose();

            if (_reportsServerInstance2006 != null)
                _reportsServerInstance2006.Dispose();
        }

        #endregion
    }
}
