using System;

namespace SSISReportGeneratorTask100.ReportingHandlers
{
    public class ComboItem
    {
        /// <summary>
        /// Gets or sets the binding value.
        /// </summary>
        /// <value>The binding value.</value>
        public object BindingValue { get; private set; }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <value>The display value.</value>
        public object DisplayValue { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboItem"/> class.
        /// </summary>
        /// <param name="aBindingValue">A binding value.</param>
        /// <param name="aDisplayValue">A display value.</param>
        public ComboItem(object aBindingValue, object aDisplayValue)
        {
            BindingValue = aBindingValue;
            DisplayValue = aDisplayValue;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            return Convert.ToString(DisplayValue);
        }
    }
}