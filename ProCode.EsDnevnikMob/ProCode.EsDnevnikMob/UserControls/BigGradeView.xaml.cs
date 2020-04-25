using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProCode.EsDnevnikMob.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BigGradeUserView : AbsoluteLayout
    {
        public BigGradeUserView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// GradeItemProperty should be binded with TimeLineEvent or 
        /// </summary>
        public static readonly BindableProperty GradeObjectProperty = BindableProperty.Create(
            propertyName: nameof(GradeObject),
            returnType: typeof(BigGradeUserView),
            declaringType: typeof(BigGradeUserView),
            defaultBindingMode: BindingMode.OneWay
            );

        public object GradeObject { 
            get { return GetValue(GradeObjectProperty); }
            set { SetValue(GradeObjectProperty, value); }
        }
    }
}