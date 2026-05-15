using Capex.Models.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


namespace Capex.Utilities.Common
{
    public class CaptchaGenerator
    {
        private static string CaptType = "0";
        private static string CaptStrength ="2";
        private static string CaptLength = "6";
        private static string CaptBgColor = "aliceblue";
        private static string CaptTextColor = "Green";
  

        CaptchaType ctyp; CaptchaStrength csth; int clen; Color cbgcolor; Color ctxtcol;        
        static public byte[] ImageToBinary(System.Drawing.Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }     

        public CaptchaWithCipher CaptchaWithCipher()
        {
            switch (CaptType)
            {
                case "0":
                    ctyp = CaptchaType.NumericOnly;
                    break;
                case "1":
                    ctyp = CaptchaType.LetterOnly;
                    break;
                case "2":
                case "":
                    ctyp = CaptchaType.Both;
                    break;
                default:
                    ctyp = CaptchaType.NumericOnly;
                    break;
            }
            switch (CaptStrength)
            {
                case "0":
                    csth = CaptchaStrength.Normal;
                    break;
                case "1":
                    csth = CaptchaStrength.Intermidiate;
                    break;
                case "3":
                    csth = CaptchaStrength.Debug;
                    break;
                case "2":
                case "":
                    csth = CaptchaStrength.Strong;
                    break;
                default:
                    csth = CaptchaStrength.Normal;
                    break;
            }
            switch (CaptLength)
            {
                case "":
                    clen = 4;
                    break;
                default:
                    clen = int.Parse(CaptLength);
                    break;
            }
            switch (CaptBgColor)
            {
                case "":
                    cbgcolor = Color.White;
                    break;
                default:
                    cbgcolor = Color.FromName(CaptBgColor);
                    break;
            }
            switch (CaptTextColor)
            {
                case "":
                    ctxtcol = Color.Black;
                    break;
                default:
                    ctxtcol = Color.FromName(CaptTextColor);
                    break;
            }
            CaptchaWithCipher cm = new CaptchaWithCipher();
            string capt = "";
            if (AppSettings.Current.DefaultCaptcha=="")
            {
                capt = RandomString(ctyp, csth);
            }
            else
                capt = AppSettings.Current.DefaultCaptcha;
            if (capt.Length != 0)
            {
                Bitmap img = GenerateImage(210, 70, capt);
                if (img != null)
                {
                    cm.Captcha = capt;
                    //cm.Cipher = StringCipher.EncryptStringAES(capt);
                    cm.CaptchaBase64 = Convert.ToBase64String(ImageToBinary(img));
                    //if (true)
                    //{
                    //    cm.Captcha = capt;
                    //}
                }
            }
            return cm;
        }      

        private Bitmap GenerateImage(int width, int height, string phrase)
        {
            Bitmap captchaImg = new Bitmap(width, height);
            Random randomizer = new Random();

            using (Graphics graphic = Graphics.FromImage(captchaImg))
            {
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.TextRenderingHint = TextRenderingHint.AntiAlias;

                // Set height and width of captcha image
                Rectangle rect = new Rectangle(0, 0, width, height);
                HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.Black, Color.Gray);
                graphic.FillRectangle(hatchBrush, rect);

                // Rotate text a little bit
                graphic.RotateTransform(-3);
                graphic.DrawString(phrase, new Font("Ex0 2", 30), new SolidBrush(Color.White), 15, 15);

                graphic.Flush();
            }

            return captchaImg;
        }
        private static Random random = new Random();
        public string RandomString(CaptchaType ctyp, CaptchaStrength csth)
        {
            const string digit = "123456789";
            const string letter = "ABCDEFGHJKMNPQRSTUVWXYZ";
            string chars = "";
            int length = clen;

            switch (ctyp)
            {
                case CaptchaType.NumericOnly:
                    chars = digit;
                    break;
                case CaptchaType.LetterOnly:
                    chars = letter.ToLower();
                    break;
                case CaptchaType.Both:
                    chars = digit + letter.ToLower();
                    break;
                default:
                    chars = "!@#$%^&*()";
                    break;
            }

            switch (csth)
            {
                case CaptchaStrength.Debug:
                    length = 2;
                    break;
                case CaptchaStrength.Normal:
                    length = 4;
                    break;
                case CaptchaStrength.Intermidiate:
                    length =4;
                    break;
                case CaptchaStrength.Strong:
                    length = 4;
                    chars = digit + letter + letter.ToLower();
                    break;
                default:
                    length = 4;
                    break;
            }

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public class CaptchaWithCipher
    {
        public string Cipher { get; set; }
        public string CaptchaBase64 { get; set; }
        public string Captcha { get; set; }
    }
       public enum CaptchaType
    {
        NumericOnly,
        LetterOnly,
        Both
    }
    public enum CaptchaStrength
    {
        Normal,
        Intermidiate,
        Strong,
        Debug
    }
}
