using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Security;
using System.Security.Permissions;
using System.Web;

namespace ETC.Sharepoint
{
    /// <summary>
    /// provides a custom DateTime field that uses 1 minute increments
    /// based on SPTimeOnly by Thiwanka Manamendra, University of Colombo School of Computing - Sri Lanka http://www.thiwanka.com thiwanka@thiwanka.com
    /// SPTimeOnly has only time fields using the typical 5 minutes increments
    /// DateTimeByMinuteFieldControl adds the DateTimeControl's DatePicker and sets the DateTimeControl to DateOnly
    /// </summary>
    [CLSCompliant(false)]
    [Guid("ced6bca2-a368-4bbf-b51b-4ddd927d499f")]
    public class DateTimeByMinuteFieldControl : BaseFieldControl
    {
        #region Controls
        protected DropDownList DropDownHH;
        protected DropDownList DropDownMM;
        protected DateTimeControl TheDatePicker;
        #endregion
        
        #region DefaultTemplateName
        /// <summary>
        /// Getting Default Template Name
        /// </summary>
        protected override string DefaultTemplateName
        {
            [SharePointPermission(
                 SecurityAction.LinkDemand, ObjectModel = true)]
            [AspNetHostingPermission(SecurityAction.LinkDemand,
                    Level = AspNetHostingPermissionLevel.Minimal)]
            get { return "DateTimeByMinuteFieldRendering"; }
        }
        #endregion

        #region Value
        /// <summary>
        /// Setting and Getting the Field Control Value
        /// Use Different format in New Form and Display form
        /// New and Edit form Format : HH AM MM
        /// Dispay form Format : HH : MM AM
        /// Get the selected date from the date time picker and concatenate the time value to the end
        /// </summary>
        public override object Value
        {
            [SharePointPermission(
                  SecurityAction.LinkDemand, ObjectModel = true)]
            [AspNetHostingPermission(SecurityAction.LinkDemand,
                    Level = AspNetHostingPermissionLevel.Minimal)]
            get
            {
                EnsureChildControls();
                string theDate = TheDatePicker.SelectedDate.Date.ToShortDateString();
                string hhString = DropDownHH.Text.Trim();
                string fieldValue = string.Empty;
                string hhValue = string.Empty;
                string mValue = string.Empty;
                if (hhString.Length == 4)
                {
                    hhValue = hhString.Substring(0, 1);
                    mValue = hhString.Substring(1);

                }
                else if (hhString.Length == 5)
                {
                    hhValue = hhString.Substring(0, 2);
                    mValue = hhString.Substring(2);
                }
                fieldValue = theDate + " " + hhValue + ":" + DropDownMM.Text.Trim() + mValue;
                return fieldValue;
            } 
        #endregion


            [SharePointPermission(
                   SecurityAction.LinkDemand, ObjectModel = true)]
            [AspNetHostingPermission(SecurityAction.LinkDemand,
                    Level = AspNetHostingPermissionLevel.Minimal)]
            set
            {
                EnsureChildControls();

                string fieldValue = value.ToString();

                string currentHH = fieldValue.Substring(0, fieldValue.IndexOf(':'));
                string currentMM = fieldValue.Substring(fieldValue.IndexOf(':') + 1, 2);
                string currentM = fieldValue.Substring(fieldValue.IndexOf(':') + 4, 2);
                DropDownHH.Text = currentHH + " " + currentM;
                DropDownMM.Text = currentMM;

                TheDatePicker.SelectedDate = System.DateTime.Parse(fieldValue.Substring(0, fieldValue.IndexOf(' ')));
 
            }
        }

        #region Focus
        [SharePointPermission(
                SecurityAction.LinkDemand, ObjectModel = true)]
        [AspNetHostingPermission(SecurityAction.LinkDemand,
                   Level = AspNetHostingPermissionLevel.Minimal)]
        public override void Focus()
        {
            TheDatePicker.Focus();
        } 
        #endregion

        #region CreateChildControls
        /// <summary>
        /// Disply form Show Field Value.
        /// Edit and New forms. show the Drop Down List to pick the time and the date picker to pick the date
        /// </summary>
        [SharePointPermission( SecurityAction.LinkDemand, ObjectModel = true)]
        [AspNetHostingPermission(SecurityAction.LinkDemand,Level = AspNetHostingPermissionLevel.Minimal)]
        protected override void CreateChildControls()
        {
            if (Field == null)
                return;

            base.CreateChildControls();

            if (ControlMode == SPControlMode.Display)

                return;


            TheDatePicker = (DateTimeControl)TemplateContainer.FindControl("TheDatePicker");
            TheDatePicker.DateOnly = true;
            if (TheDatePicker.Equals(null))
            {
                throw new ArgumentException("Corrupt DateTimeByMinuteTemplate - Date Missing");
            }
            TheDatePicker.TabIndex = TabIndex;
            TheDatePicker.ToolTip = Field.Title + " Date";

            DropDownHH = (DropDownList)TemplateContainer.FindControl("DropDownHH");
            if (DropDownHH.Equals(null))
            {
                throw new ArgumentException("Corrupt DateTimeByMinuteTemplate - HH Missing");
            }
            DropDownHH.TabIndex = TabIndex;
            DropDownHH.CssClass = CssClass;
            DropDownHH.ToolTip = Field.Title + " Hour";

            DropDownMM = (DropDownList)TemplateContainer.FindControl("DropDownMM");

            if (DropDownMM.Equals(null))
            {
                throw new ArgumentException("Corrupt DateTimeByMinuteTemplate - MM Missing");
            }
            DropDownMM.TabIndex = TabIndex;
            DropDownMM.CssClass = CssClass;
            DropDownMM.ToolTip = Field.Title + " Minute";


            if (ControlMode == SPControlMode.New)
            {
                DropDownHH.Text = Field.GetCustomProperty("DefaultHH").ToString();
                DropDownMM.Text = Field.GetCustomProperty("DefaultMM").ToString();
            }
        } 
        #endregion
    }
}
