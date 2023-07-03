using Microsoft.AspNetCore.Mvc.Rendering;
using PriceOptimizerCoreApplication.web.Models;
using System;
using System.IO;
using System.Threading.Tasks;
namespace PriceOptimizerCoreApplication.web.Util
{
    public static  class Utility
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var routeAction = Convert.ToString(routeData.Values["action"]);
            var routeController = Convert.ToString(routeData.Values["controller"]);
            var returnActive = (controller == routeController && (action == routeAction || routeAction == "Index"));
            return returnActive?"nav-item active":"nav-item";
        }
        public static string MakeEmailBody(Email email)
        {
            var bodyString = "<!DOCTYPE html><html><head><meta http-equiv ='Content-Type' content='text/html; charset=utf-8' /><title> Email Query </title><style type ='text/css'>body { margin: 0; padding: 0; min - width: 100 % !important; }img { height: auto; }.content { width: 100 %; max - width: 600px; }.header { padding: 40px 30px 20px 30px; }.innerpadding { padding: 30px 30px 30px 30px; }.borderbottom {border - bottom: 1px solid #f2eeed;}.subhead {font - size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px;}.h1, .h2, .bodycopy {color: #153643; font-family: sans-serif;}.h1 { font - size: 33px; line - height: 38px; font - weight: bold; }.h2 { padding: 0 0 15px 0; font - size: 24px; line - height: 28px; font - weight: bold; } .bodycopy { font - size: 16px; line - height: 22px; }.button { text - align: center; font - size: 18px; font - family: sans - serif; font - weight: bold; padding: 0 30px 0 30px; }.button a { color: #ffffff; text-decoration: none;}.footer { padding: 20px 30px 15px 30px; }.footercopy { font - family: sans - serif; font - size: 14px; color: #ffffff;} .footercopy a {color: #ffffff; text-decoration: underline;}@media only screen and(max-width: 550px), screen and(max-device - width: 550px) {body[yahoo].hide { display: none!important; }body[yahoo].buttonwrapper { background - color: transparent!important; }body[yahoo].button { padding: 0px!important; }body[yahoo].button a {background - color: #e05443; padding: 15px 15px 13px!important;} body[yahoo].unsubscribe {display: block; margin - top: 20px; padding: 10px 50px; background: #2f3942; border-radius: 5px; text-decoration: none!important; font-weight: bold;}}</ style ></ head >< body yahoo bgcolor ='#f6f8f1' >< table width = '100%' bgcolor ='#f6f8f1' border = '0' cellpadding ='0' cellspacing ='0'><tr><td>< table bgcolor ='#ffffff' class='content' align='center' cellpadding='0' cellspacing='0' border='0'><tr><td bgcolor ='#c7d8a7' class='header'><table width ='70' align='left' border='0' cellpadding='0' cellspacing='0'><tr><td height ='70' style='padding: 0 20px 20px 0;'><img class='fix' src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/210284/icon.gif' width='70' height='70' border='0' alt='' /></td></tr></table><table class='col425' align='left' border='0' cellpadding='0' cellspacing='0' style='width: 100%; max-width: 425px;'><tr><td height ='70' >< table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='subhead' style='padding: 0 0 0 3px;'>User Query</td></tr><tr><td class='h1' style='padding: 5px 0 0 0;'>User name is -" + email.Name + "</td></tr></table>";
            bodyString+="</td></tr></table></td></tr><tr><td class='innerpadding borderbottom'><table width ='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='h2'>Subject</td></tr><tr> <td class='h2'> Name </td><td>"+email.Name+"</td> </tr><tr><td class='h2'>Email</td><td>"+email.To+"</td></tr><tr><td class='bodycopy'>Body Message</td><td>"+email.Body+"</td></tr></table></td></tr><tr><td class='footer' bgcolor='#44525f'><table width ='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align ='center' class='footercopy'>&reg; Someone, somewhere 20XX<br/><a href ='#' class='unsubscribe'><font color ='#ffffff' > Unsubscribe </ font ></ a >< span class='hide'>from this newsletter instantly</span></td></tr><tr><td align ='center' style='padding: 20px 0 0 0;' >< table border='0' cellspacing='0' cellpadding='0' ><tr>< td width='37' style='text-align: center; padding: 0 10px 0 10px;' ><a href='https://www.facebook.com/Atsolutionhubcom-106938227818048/?modal=admin_todo_tour' >< img src= 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/210284/facebook.png' width='37' height='37' alt='Facebook' border='0' /></ a ></ td ><td width='37' style='text-align: center; padding: 0 10px 0 10px;' >< a href='https://twitter.com/priyankadholey' >< img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/210284/twitter.png' width='37' height='37' alt='Twitter' border='0' /></ a ></ td ></ tr ></ table ></ td ></ tr ></ table ></ td ></ tr ></ table ></ td ></ tr ></ table ></ body ></ html > ";
            return bodyString; 
        }
        public static async Task<bool> LogErrorMessage(Exception e, string filePath,string ControllerName,string ActionName)
        {
        //D:\Accounts\PriceOptimizerCoreApplication\PriceOptimizerCoreApplication.web\wwwroot\ErrorLogs\Error.txt
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Date : " + DateTime.Now.ToString());
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("-----------------------ControllerName-----------------------------------ActionName-------------------");
                    await writer.WriteLineAsync($"ControllerName-{ControllerName}");
                    await writer.WriteLineAsync($"ActionName-{ActionName}");
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    while (e != null)
                    {
                        await writer.WriteLineAsync(e.GetType().FullName);
                        await writer.WriteLineAsync("Message : " + e.Message);
                        await writer.WriteLineAsync("StackTrace : " + e.StackTrace);

                        e = e.InnerException;
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Date : " + DateTime.Now.ToString());
                    await writer.WriteLineAsync();
                    while (ex != null)
                    {
                        await writer.WriteLineAsync(ex.GetType().FullName);
                        await writer.WriteLineAsync("Message : " + ex.Message);
                        await writer.WriteLineAsync("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
            }
            return true;
        }

        public static async Task<bool> LogRemortIp( string filePath, string Ip)
        {
            //D:\Accounts\PriceOptimizerCoreApplication\PriceOptimizerCoreApplication.web\wwwroot\ErrorLogs\Error.txt
            try
            {
             
                
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Date : " + DateTime.Now.ToString());
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Remort IP : " + Ip);
                    await writer.WriteLineAsync("----------------------------------------Thank You-------------------------------------");
                }
              
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Date : " + DateTime.Now.ToString());
                    await writer.WriteLineAsync();
                    while (ex != null)
                    {
                        await writer.WriteLineAsync(ex.GetType().FullName);
                        await writer.WriteLineAsync("Message : " + ex.Message);
                        await writer.WriteLineAsync("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
            }
            return true;
        }
        public static async Task<bool> Log(string filePath, string _host, string _from,string Alias,int i)
        {
            //D:\Accounts\PriceOptimizerCoreApplication\PriceOptimizerCoreApplication.web\wwwroot\ErrorLogs\Error.txt
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Date : " + DateTime.Now.ToString());
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("---------Call From----"+i);
                    await writer.WriteLineAsync("Remort Host : " + _host);
                    await writer.WriteLineAsync("Remort From : " + _from);
                    await writer.WriteLineAsync("Remort Alias : " + Alias);
                    await writer.WriteLineAsync("----------------------------------------Thank You-------------------------------------");
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Date : " + DateTime.Now.ToString());
                    await writer.WriteLineAsync();
                    while (ex != null)
                    {
                        await writer.WriteLineAsync(ex.GetType().FullName);
                        await writer.WriteLineAsync("Message : " + ex.Message);
                        await writer.WriteLineAsync("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
            }
            return true;
        }
    }
}
