//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FourSeasons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    
    // Generate Id:4e45fb8a-1191-4778-bccb-ea95da25e179
    public partial class UIMainPanel
    {
        
        public const string NAME = "UIMainPanel";
        
        private UIMainPanelData mPrivateData = null;
        
        public UIMainPanelData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new UIMainPanelData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            mData = null;
        }
    }
}
