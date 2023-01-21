using System;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using DigiD.Common.EID.Helpers;
using DigiD.Common.Models;
using Xamarin.Forms;

namespace DigiD.Common.Helpers
{
    public static class PinCodeHelper
    {
        public static PincodeValidationResult IsPinCodeComplexEnough(SecureString pin)
        {
            var result = new PincodeValidationResult
            {
                Success = true,
                Header = Device.Idiom == TargetIdiom.Desktop ? AppResources.DA1xx_Header : AppResources.PincodeComplexHeader
            };

            if (pin.IsIncreasing())
            {  
                result.Success = false;
                result.Message = Device.Idiom == TargetIdiom.Desktop ? AppResources.DA110_Message : AppResources.PincodeIncreasingError;
            } 
            else if (pin.IsDecreasing()) 
            {
                result.Success = false;
                result.Message = Device.Idiom == TargetIdiom.Desktop ? AppResources.DA111_Message :AppResources.PincodeDecreasingError;
            } 
            else if (pin.AreEqual()) 
            {
                result.Success = false;
                result.Message = Device.Idiom == TargetIdiom.Desktop ? AppResources.DA109_Message : AppResources.PincodeDistictError;
            }
				
            return result;
        }

        public static string GenerateMaskCode()
        {
            using (var random = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[5];
                random.GetBytes(byteArray);

                //convert 5 bytes to an integer
                var randomInteger = BitConverter.ToInt32(byteArray, 0);
                //convert value to positive
                randomInteger = Math.Abs(randomInteger);

                //Take the first 5 chars of the integer
                return randomInteger.ToString(CultureInfo.InvariantCulture).Substring(0, 5);
            }
        }

        public static string MaskPin(SecureString pin, string mask)
        {
            if (pin == null)
                throw new ArgumentNullException(nameof(pin));

            var maskCode = mask.ToCharArray();
            var pinValue = pin.ToPlain().ToCharArray();

            var pinResult = new StringBuilder();

            for (var x = 0; x < 5; x++)
            {
                try
                {
                    var val1 = int.Parse(maskCode[x].ToString(), CultureInfo.InvariantCulture);
                    var val2 = int.Parse(pinValue[x].ToString(), CultureInfo.InvariantCulture);
                    var sum = val1 + val2;
                    pinResult.Append(sum % 10);
                }
                catch (Exception)
                {
                    //Entered pin is invalide
                }
            }

            return pinResult.ToString();
        }
    }
}
