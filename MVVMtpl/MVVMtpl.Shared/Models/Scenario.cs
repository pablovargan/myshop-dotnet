namespace MVVMtpl.Models
{
    using MVVMtpl.Base;
    using System;
    using Windows.UI.Xaml.Controls;

    public class Scenario : ObservableObject
    {
        private string tittle;
        private Type classType;
        public string Title
        {
            get { return this.tittle; }
            set { this.Set(ref this.tittle, value); }
        }
        public Type ClassType
        {
            get { return this.classType; }
            set { this.Set(ref this.classType, value); }
        }
    }
}
