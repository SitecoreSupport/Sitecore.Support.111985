using Sitecore.Diagnostics;
using Sitecore.Shell;
using System;
using System.Collections.Generic;

namespace Sitecore.Support.Shell.Applications.ContentManager.Dialogs.OneColumnPreview
{
    public class OneColumnPreviewForm : Sitecore.Shell.Applications.ContentManager.Dialogs.OneColumnPreview.OneColumnPreviewForm
    {
        protected override void OnPreRender(EventArgs e)
        {
            Sitecore.Security.Accounts.User user = Sitecore.Context.User;
            Sitecore.Security.UserProfile profile = user.Profile;
            List<string> keys = profile.GetCustomPropertyNames();
            string key = "/" + user.Name + "/UserOptions.ContentEditor.RenderCollapsedSections";
            bool hasCustomerOwnSetting = keys.Contains(key);

            bool renderCollapsedSections = UserOptions.ContentEditor.RenderCollapsedSections;
            try
            {
                UserOptions.ContentEditor.RenderCollapsedSections = true;
                base.OnPreRender(e);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString(), this);
            }
            finally
            {
                UserOptions.ContentEditor.RenderCollapsedSections = renderCollapsedSections;
                if (!hasCustomerOwnSetting)
                {
                    profile.RemoveCustomProperty(key);
                    profile.Save();
                }
            }
        }
    }
}

