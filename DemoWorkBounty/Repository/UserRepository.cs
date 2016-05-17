using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;
using DemoWorkBounty;
using System.Net.Mail;

namespace DemoWorkBounty.Repository
{
    public class UserRepository : ApiController
    {
        private WorkBountyDBEntities6 entity = new WorkBountyDBEntities6();

        public UserInfo UserLogin(UserInfo userLoginData)
        {
            try
            {
                var checkLoginData = entity.UserInfoes.Where(a => a.Email.Equals(userLoginData.Email) && a.Password.Equals(userLoginData.Password)).FirstOrDefault();

                if (checkLoginData == null)
                {
                    return checkLoginData;
                }
                else
                {
                    return checkLoginData;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public UserInfo AddUserDetails(UserInfo userSignupData)
        {
            try
            {
                var checkUserSignupInfo = entity.UserInfoes.Where(a => a.Email.Equals(userSignupData.Email)).FirstOrDefault();
                if (checkUserSignupInfo == null)
                {
                    entity.UserInfoes.Add(userSignupData);
                    entity.SaveChanges();
                    return userSignupData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<UserProfileInfo> ViewUserProfileDetails(int currentUserData)
        {
            try
            {
                
                    var getUserDetails = entity.UserInfoes.Where(s => s.UserID == currentUserData).Select(s => new UserProfileInfo { Email = s.Email, FirstName = s.FirstName, LastName = s.LastName, DateOfBirth = s.DateOfBirth, InterestedKeywords = s.InterestedKeywords, PhoneNumber = s.PhoneNumber }).ToList();
                    return getUserDetails;
               
            }
            catch (Exception)
            {
                return null;
            }

        }


        public UserInfo ForgotPasswordValidation(UserInfo userLoginData)
        {
            try
            {
                var checkLoginData = entity.UserInfoes.Where(a => a.Email.Equals(userLoginData.Email)).FirstOrDefault();

                if (checkLoginData == null)
                {
                    return checkLoginData;
                }
                else
                {
                    var getPasswordDetail = entity.UserInfoes.Where(a => a.Email.Equals(userLoginData.Email)).Select(a => a.Password).FirstOrDefault();
                    MailMessage mm = new MailMessage("help.workbounty@gmail.com", userLoginData.Email);
                    mm.Subject = "Workbounty Password Recovery";
                    mm.Body = string.Format("Hi {0},<br /><br />Your password is {1}.<br /><br />Thank You.", checkLoginData.Email, getPasswordDetail);
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    NetworkCred.UserName = "help.workbounty@gmail.com";
                    NetworkCred.Password = "workbountyproject1234";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);

                    return checkLoginData;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }



    }
}