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


        public static string Ticket(string fullName,string fromCity,string toCity, DateTime departureTime,
     string busPlate,string busType,List<string> seatNumbers,string qrCodeBase64, Guid bookingId)
        {
            var seatsHtml = string.Join("", seatNumbers.Select(s =>
                "<span style='display:inline-block;background:#e8f0fe;color:#1a73e8;border-radius:6px;padding:6px 14px;margin:4px;font-weight:bold;'>" + s + "</span>"));

            var qrHtml = string.IsNullOrEmpty(qrCodeBase64)
                ? ""
                : "<img src='data:image/png;base64," + qrCodeBase64 + "' style='width:160px;height:160px;margin:16px auto;display:block;' />";

            return @"<!DOCTYPE html>
<html dir='rtl' lang='ar'>
<head>
  <meta charset='UTF-8'/>
  <style>
    body { font-family: Arial, sans-serif; background:#f4f4f4; margin:0; padding:20px; }
    .container { max-width:480px; margin:auto; background:#fff; border-radius:12px; overflow:hidden; }
    .header { background:#1a73e8; color:#fff; text-align:center; padding:24px; }
    .logo { font-size:24px; font-weight:bold; margin-bottom:4px; }
    .badge { background:rgba(255,255,255,0.2); border-radius:20px; padding:4px 14px; font-size:12px; display:inline-block; margin-top:8px; }
    .body { padding:24px; }
    .route { display:flex; justify-content:space-between; align-items:center; text-align:center; margin-bottom:20px; }
    .city { font-size:18px; font-weight:bold; color:#222; }
    .arrow { color:#1a73e8; font-size:20px; }
    table { width:100%; border-collapse:collapse; margin-top:12px; }
    td { padding:10px 0; border-bottom:1px solid #eee; font-size:14px; color:#333; }
    td.label { color:#888; width:40%; }
    .seats-wrap { text-align:center; margin-top:16px; }
    .qr-wrap { text-align:center; margin-top:8px; }
    .qr-note { font-size:11px; color:#999; text-align:center; margin-top:4px; }
    .footer { font-size:12px; color:#999; margin-top:20px; text-align:center; padding-bottom:20px; }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>
      <div class='logo'>AMBus</div>
      <div class='badge'>✓ تذكرة مؤكدة</div>
    </div>
    <div class='body'>
      <p>مرحباً <strong>" + fullName + @"</strong>، رحلتك جاهزة 🚌</p>
      <div class='route'>
        <div class='city'>" + fromCity + @"</div>
        <div class='arrow'>&#8592;</div>
        <div class='city'>" + toCity + @"</div>
      </div>
      <table>
        <tr><td class='label'>موعد الانطلاق</td><td>" + departureTime.ToString("yyyy-MM-dd HH:mm") + @"</td></tr>
        <tr><td class='label'>رقم الباص</td><td>" + busPlate + @"</td></tr>
        <tr><td class='label'>نوع الباص</td><td>" + busType + @"</td></tr>
        <tr><td class='label'>رقم الحجز</td><td style='direction:ltr; text-align:right;'>" + bookingId + @"</td></tr>
      </table>
      <div class='seats-wrap'>" + seatsHtml + @"</div>
      <div class='qr-wrap'>" + qrHtml + @"</div>
      <div class='qr-note'>اعرض كود QR عند الصعود للباص</div>
    </div>
  </div>
  <div class='footer'>AMBus Trip Manager &copy; " + DateTime.UtcNow.Year + @"</div>
</body>
</html>";
        }
    }
}
