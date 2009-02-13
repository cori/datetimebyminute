using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Security;

namespace ETC.Sharepoint
{
  
    /// <summary>
    /// provides a custom DateTime field that uses 1 minute increments
    /// based on SPTimeOnly by Thiwanka Manamendra, University of Colombo School of Computing - Sri Lanka http://www.thiwanka.com thiwanka@thiwanka.com
    /// </summary>

    /// TODO: add reasoning/educational-leve documentation
    [CLSCompliant(false)]
    [Guid("17419dea-75f0-45b3-ad3e-ffcd8d91055b")]
    public class DateTimeByMinuteField : SPFieldText
    {
        public DateTimeByMinuteField(SPFieldCollection fields, string fieldName)
            : base(fields, fieldName)
        {
        }

        public DateTimeByMinuteField(SPFieldCollection fields, string typeName, string displayName)
            : base(fields, typeName, displayName)
        {
        }

        public override BaseFieldControl FieldRenderingControl
        {
            [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
            get
            {
                BaseFieldControl fieldControl = new DateTimeByMinuteFieldControl();
                fieldControl.FieldName = this.InternalName;

                return fieldControl;
            }
        }
    }
}
