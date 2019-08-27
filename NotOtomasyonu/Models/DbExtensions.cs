using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotOtomasyonu.Models
{
    //Extension Method 
    public static partial class Html
    {
        public const int SinavKatPuani = 60;
        public const int SozluKatPuani = 40;

        public static double? NotHesapla(this HtmlHelper html, NotDb notDb)
        {
            int count = 0;
            double? notOrt = null;
            if (!IsNull(notDb.Sinav1))
            {
                if (!IsNull(notDb.Sozlu1))
                {
                    count++;
                    notOrt = (notDb.Sinav1.Value * SinavKatPuani + notDb.Sozlu1.Value * SozluKatPuani) / 100;

                    if (!IsNull(notDb.Sinav2))
                    {
                        if (!IsNull(notDb.Sozlu2))
                        {
                            count++;
                            notOrt += (notDb.Sinav2.Value * SinavKatPuani + notDb.Sozlu2.Value * SozluKatPuani) / 100;
                            if (!IsNull(notDb.Sinav3))
                            {
                                if (!IsNull(notDb.Sozlu3))
                                {
                                    count++;
                                    notOrt += (notDb.Sinav3.Value * SinavKatPuani + notDb.Sozlu3.Value * SozluKatPuani) / 100;

                                }
                            }
                        }
                    }
                }
            }
            return notOrt.HasValue ? notOrt / count : null;
        }

        public static bool IsNull(int? value)
        {
            return value == null;
        }
    }
}