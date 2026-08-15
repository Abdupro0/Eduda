using DevExpress.LookAndFeel;
using Eduda.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eduda
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // تعيين ثيم Dark مناسب
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Dark Gray"); // مثال على Skin Dark

            // تعيين Palette للـ Skin
            DevExpress.Skins.SkinManager.EnableFormSkins();

            // إذا تريد تعديل لون معين (اختياري)
            UserLookAndFeel.Default.SkinMaskColor = Color.Violet;
            Application.Run(new FRMAddStudent());

    }
        
    }
}
