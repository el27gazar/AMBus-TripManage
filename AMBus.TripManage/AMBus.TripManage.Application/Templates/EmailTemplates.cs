using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMBus.TripManage.Application.Templates
{
    public static class EmailTemplates
    {
        public static string ResetPassword(string fullName, string otpCode)
        {
            return @"<!DOCTYPE html>
<html dir='rtl' lang='ar'>
<head>
  <meta charset='UTF-8'/>
  <style>
    body { font-family: Arial, sans-serif; background:#f4f4f4; margin:0; padding:20px; }
    .container { max-width:480px; margin:auto; background:#fff; border-radius:12px; padding:32px; text-align:center; }
    .logo { font-size:24px; font-weight:bold; color:#1a73e8; margin-bottom:16px; }
    h2 { color:#222; }
    p { color:#555; line-height:1.6; }
    .otp { font-size:36px; font-weight:bold; letter-spacing:8px; color:#1a73e8; background:#e8f0fe; border-radius:8px; padding:16px 32px; display:inline-block; margin:16px 0; }
    .warning { font-size:12px; color:#999; margin-top:24px; }
  </style>
</head>
<body>
  <div class='container'>
    <div class='logo'>AMBus</div>
    <h2>إعادة تعيين كلمة المرور</h2>
    <p>مرحباً <strong>" + fullName + @"</strong>،</p>
    <p>رمز إعادة التعيين الخاص بك، صالح لمدة <strong>10 دقائق</strong>:</p>
    <div class='otp'>" + otpCode + @"</div>
    <p class='warning'>إذا لم تطلب هذا، تجاهل الرسالة.</p>
  </div>
</body>
</html>";
        }

        public static string ConfirmEmail(string fullName, string otpCode)
        {
            return @"<!DOCTYPE html>
<html dir='rtl' lang='ar'>
<head>
  <meta charset='UTF-8'/>
  <style>
    body { font-family: Arial, sans-serif; background:#f4f4f4; margin:0; padding:20px; }
    .container { max-width:480px; margin:auto; background:#fff; border-radius:12px; padding:32px; text-align:center; }
    .logo { font-size:24px; font-weight:bold; color:#1a73e8; margin-bottom:16px; }
    h2 { color:#222; }
    p { color:#555; line-height:1.6; }
    .otp { font-size:36px; font-weight:bold; letter-spacing:8px; color:#1a73e8; background:#e8f0fe; border-radius:8px; padding:16px 32px; display:inline-block; margin:16px 0; }
    .warning { font-size:12px; color:#999; margin-top:24px; }
  </style>
</head>
<body>
  <div class='container'>
    <div class='logo'>AMBus</div>
    <h2>تأكيد البريد الإلكتروني</h2>
    <p>مرحباً <strong>" + fullName + @"</strong>،</p>
    <p>رمز التأكيد الخاص بك:</p>
    <div class='otp'>" + otpCode + @"</div>
    <p class='warning'>صالح لمدة 10 دقائق فقط.</p>
  </div>
</body>
</html>";
        }


        public static string BookingConfirmed( string fullName, string fromCity,string toCity, DateTime departureTime,
    string seatNumbers, decimal amount,string currency, Guid bookingId)
        {
            return @"<!DOCTYPE html>
    <html dir='rtl' lang='ar'>
        <head>
         <meta charset='UTF-8'/>
             <style>
            body { font-family: Arial, sans-serif; background:#f4f4f4; margin:0; padding:20px; }
            .container { max-width:480px; margin:auto; background:#fff; border-radius:12px; padding:32px; }
             .logo { font-size:24px; font-weight:bold; color:#1a73e8; margin-bottom:16px; text-align:center; }
             h2 { color:#222; text-align:center; }
             p { color:#555; line-height:1.6; }
    .badge { background:#e6f4ea; color:#1e7e34; font-weight:bold; border-radius:20px; padding:6px 16px; display:inline-block; margin:8px 0 20px; font-size:14px; }
    .badge-wrap { text-align:center; }
    table { width:100%; border-collapse:collapse; margin-top:8px; }
    td { padding:10px 12px; border-bottom:1px solid #eee; font-size:14px; color:#333; }
    td.label { color:#888; width:40%; }
    .footer { font-size:12px; color:#999; margin-top:24px; text-align:center; }
  </style>
</head>
<body>
  <div class='container'>
    <div class='logo'>AMBus</div>
    <h2>تم تأكيد حجزك بنجاح</h2>
    <div class='badge-wrap'><span class='badge'>✓ مدفوع</span></div>
    <p>مرحباً <strong>" + fullName + @"</strong>،</p>
    <p>تم تأكيد دفعك وحجزك بنجاح. تفاصيل رحلتك:</p>
    <table>
      <tr><td class='label'>من</td><td>" + fromCity + @"</td></tr>
      <tr><td class='label'>إلى</td><td>" + toCity + @"</td></tr>
      <tr><td class='label'>موعد الانطلاق</td><td>" + departureTime.ToString("yyyy-MM-dd HH:mm") + @"</td></tr>
      <tr><td class='label'>رقم/أرقام المقعد</td><td>" + seatNumbers + @"</td></tr>
      <tr><td class='label'>المبلغ المدفوع</td><td>" + amount + " " + currency + @"</td></tr>
      <tr><td class='label'>رقم الحجز</td><td style='direction:ltr; text-align:right;'>" + bookingId + @"</td></tr>
    </table>
    <p style='margin-top:20px; text-align:center;'>نتمنى لك رحلة سعيدة 🚌</p>
    <div class='footer'>AMBus Trip Manager &copy; " + DateTime.UtcNow.Year + @"</div>
  </div>
</body>
</html>";
        }
    }
}
