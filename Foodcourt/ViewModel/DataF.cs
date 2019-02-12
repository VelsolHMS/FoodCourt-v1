using System;
using System.Linq;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Foodcourt.ViewModel
{
    public class DataF:IDataErrorInfo
    {
        public string Amount { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Description { get; set; }
        public string Companycode { get; set; }
        public string Companyname { get; set; }
        public string Mobilenumber { get; set; }
        public string Contact { get; set; }
        public string Plotno { get; set; }
        public string Pincode { get; set; }
        public string Emailid { get; set; }
        public string Mailid { get; set; }
        public string Creditlimit { get; set; }
        public string Creditdays { get; set; }
        public string Blacklisted { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public string Reportname { get; set; }
        public string Landline1 { get; set; }
        public string Landline2 { get; set; }
        public string Gst { get; set; }
        public string Planname { get; set; }
        public string Password { get; set; }
        public string Repassword { get; set; }
        public string Pax { get; set; }
        public string Paxadult { get; set; }
        public string Paxchild { get; set; }
        public string Extra { get; set; }
        public string Extraadult { get; set; }
        public string Extrachild { get; set; }
        public string Rooms { get; set; }
        public string Date { get; set; }
        public string ToDate { get; set; }
        public string Roomname { get; set; }
        public string Number { get; set; }
        public string Number1 { get; set; }
        public string Taxcode { get; set; }
        public string Toamount { get; set; }
        public string Website { get; set; }
        public string Transactionno { get; set; }
        public string Chequeno { get; set; }
        public string Percentage { get; set; }
        public string Tipamount { get; set; }
        public string Authorization { get; set; }
        public string Reservationid { get; set; }
        public string Transferroom { get; set; }
        public string Special { get; set; }
        public string Addharcard { get; set; }
        public string Pancard { get; set; }
        public string Driving { get; set; }
        public string Voterid { get; set; }
        public string Passport { get; set; }
        public string cmbresto { get; set; }
        public string Time { get; set; }
        public string Captain { get; set; }
        public string Discount { get; set; }
        public string ItemDetails { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ReportingName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string ItemDescription { get; set; }
        public static int COUNT;
        public string columnName;
        //public string QuantityZero;
        //public string Quantityone;
        //public string Quantitytwo;
        //public string Quantitythree;
        //public string Quantityfour;
        //public string Quantityfive;
        //public string Quantitysix;
        //public string Quantityseven;
        //public string Quantityeight;
        public string result = null;

        public string drive = @"[A-Z]{2}[0-9]{2}[0-9]{4}[0-9]{7}";
        public string pan = @"[A-Z]{5}[0-9]{4}[A-Z]{1}";
        public string name = @"^[a-zA-Z ]+$";
        public string percentage = @"^\d+\.?\d*$"; //"(\D)\s*([\d,]+)";
        public string discount = @"^(\d{1,4}(\.\d{1,1})?)(?<!^[0\.]+)$";
        public string reg = @"(\D)\s*([.\d,]+)";
        public string aadhar = @"^\d{4}[-]\d{4}[-]\d{4}$";
        public string voter = @"[A-Z]{3}[0-9]{7}";
        public string passport = @"^[A-PR-WY][1-9]\d\s?\d{4}[1-9]$";
        public string time = @"(([01]?[0-9]):([0-5][0-9]) ([AaPp][Mm]))";
        public string alphanumwithspace = "^[0-9A-Za-z ]+$";
        public string alphanum = "^[0-9A-Za-z]+$";
        public string amount = @"^\d+\.?\d*$";
        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        public bool IsAlphaNumeric(string result)
        {
            return result.All(char.IsLetterOrDigit);
        }
        public bool IsAlphabets(string alpa)
        {
            return alpa.All(char.IsLetter);
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                if (COUNT != 0)
                {
                    if (columnName == "Amount")
                    {

                        if (string.IsNullOrEmpty(Amount))
                        {
                            result = "Please enter valid amount";
                        }
                        else if (Regex.IsMatch(Amount, reg) && IsAlphaNumeric(Amount) == false)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Amount Must be like 1000.00";
                        }
                    }
                    if (columnName == "Percentage")
                    {

                        if (string.IsNullOrEmpty(Percentage))
                        {
                            result = "Please enter valid percentage";
                        }
                        else if (Regex.IsMatch(Percentage, percentage) && IsAlphaNumeric(Percentage) == false)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Percentage must be like 10.0%";
                        }
                    }
                    if (columnName == "Discount")
                    {

                        if (string.IsNullOrEmpty(Discount))
                        {
                            result = "Please enter valid percentage";
                        }
                        else if (Regex.IsMatch(Discount, discount) && IsAlphaNumeric(Discount) == false)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Percentage must be like 10.0%";
                        }
                    }
                    if (columnName == "Name")
                    {
                        if (string.IsNullOrEmpty(Name))
                        {
                            result = "Please enter a Name";
                        }
                        else if (Regex.IsMatch(Name, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only alphabets";
                        }
                    }
                    if(columnName=="cmbresto")
                    {
                        if(string.IsNullOrEmpty(cmbresto))
                        {
                            result = "Please select a Resto name";
                        }
                        else if(IsAlphaNumeric(cmbresto)==true)
                        {
                            result = null;
                        }
                    }
                    if (columnName == "Captain")
                    {
                        if (string.IsNullOrEmpty(Captain))
                        {
                            result = "Please select captain name";
                        }
                        else if (IsAlphaNumeric(Captain) == true)
                        {
                            result = null;
                        }
                    }
                    if (columnName=="Time")
                    {
                        if(string.IsNullOrEmpty(Time))
                        {
                            result = "Please select Time";
                        }
                        else if(Regex.IsMatch(Time,time)&&IsAlphaNumeric(Time)==false)
                        {
                            result = null;
                        }
                    }
                    if (columnName == "Name1")
                    {
                        if (string.IsNullOrEmpty(Name1))
                        {
                            result = "Please enter a Name";
                        }
                        else if (Regex.IsMatch(Name1, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only alphabets";
                        }
                    }
                    if (columnName == "Description")
                    {
                        if (Description.Length <= 35)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only alphabets";
                        }
                    }
                    if (columnName == "Companycode")
                    {
                        if (string.IsNullOrEmpty(Companycode))
                        {
                            result = "Code cannot be empty";
                        }
                        else if (IsAlphaNumeric(Companycode) == true)
                        {
                            result = null;
                        }
                    }
                    if (columnName == "Companyname")
                    {
                        if (string.IsNullOrEmpty(Companyname))
                        {
                            result = "Company name cannot be empty";
                        }
                        else if (Regex.IsMatch(Companyname, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please enter only alphabets";
                        }
                    }
                    if (columnName == "Mobilenumber")
                    {
                        if (string.IsNullOrEmpty(Mobilenumber))
                        {
                            result = "Mobile number cannot be empty";
                        }
                        else if (IsNumeric(Mobilenumber) == true && Mobilenumber.Length == 10)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please verify mobile number";
                        }
                    }
                    if (columnName == "Plotno")
                    {
                        if (string.IsNullOrEmpty(Plotno))
                        {
                            result = "Door/Plot no cannot be empty";
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    if (columnName == "Pincode")
                    {
                        if (string.IsNullOrEmpty(Pincode))
                        {
                            result = "Please provide a pincode";
                        }
                        else if (IsNumeric(Pincode) == true && Pincode.Length == 6)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please check the pincode";
                        }
                    }
                    if (columnName == "Contact")
                    {
                        if (string.IsNullOrEmpty(Contact))
                        {
                            result = "Contact number cannot be empty";
                        }
                        else if (IsNumeric(Contact) == true && Contact.Length == 10)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please verify contact number";
                        }
                    }
                    if (columnName == "Emailid")
                    {
                        string mail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                        if (string.IsNullOrEmpty(Emailid))
                        {
                            result = "Please provide a email address";
                        }
                        else if (Regex.IsMatch(Emailid, mail))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please check the email address ";
                        }
                    }
                    if (columnName == "Mailid")
                    {
                        string mail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                        if (Mailid == "")
                        {
                            result = null;
                        }
                        else if (Regex.IsMatch(Mailid, mail))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please provide a email address ";
                        }
                    }
                    if (columnName == "Creditlimit")
                    {
                        if (string.IsNullOrEmpty(Creditlimit))
                        {
                            result = "Please enter a credit limit";
                        }
                        else if (IsNumeric(Creditlimit) == true)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please enter only numbers";
                        }
                    }
                    if (columnName == "Creditdays")
                    {
                        if (string.IsNullOrEmpty(Creditdays))
                        {
                            result = "Please enter no of credit days";
                        }
                        else if (IsNumeric(Creditdays) == true)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please enter numbers only";
                        }
                    }
                    if (columnName == "Blacklisted")
                    {
                        if (string.IsNullOrEmpty(Blacklisted))
                        {
                            result = "please provide a name";
                        }
                        else if (Regex.IsMatch(Blacklisted, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please enter only alphabets";
                        }
                    }
                    if (columnName == "Reason")
                    {
                        if (string.IsNullOrEmpty(Reason))
                        {
                            result = "Reason cannot be empty";
                        }
                        else if (Regex.IsMatch(Reason, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only alphabets";
                        }
                    }
                    if (columnName == "Remarks")
                    {
                        if (string.IsNullOrEmpty(Remarks))
                        {
                            result = "Please enter a remarks";
                        }
                        else if (Regex.IsMatch(Remarks, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only alphabets";
                        }
                    }
                    if (columnName == "Reportname")
                    {
                        if (string.IsNullOrEmpty(Reportname))
                        {
                            result = "Please enter a name";
                        }
                        else if (Regex.IsMatch(Reportname, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter a valid name";
                        }
                    }
                    if (columnName == "Landline1")
                    {
                        if (string.IsNullOrEmpty(Landline1))
                        {
                            result = "Please enter Landline number ";
                        }
                        else
                        {
                            string a = "[0-9 -]{4}[0-9]{6,8}";
                            if (Regex.IsMatch(Landline1, a))
                            {
                                result = null;
                            }
                            else { result = "please enter valid landline number (example 040-00000000) "; }
                        }
                    }
                    if (columnName == "Landline2")
                    {
                        string a = "[0-9 -]{4}[0-9]{6,8}";
                        if (Regex.IsMatch(Landline2, a))
                        {
                            result = null;
                        }
                        else result = "please enter valid landline number (example 040-00000000) ";

                    }
                    if (columnName == "Gst")
                    {
                        string a = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
                        if (Regex.IsMatch(Gst, a))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "please enter valid GST number (example 12ABCDE1234B1ZA)";
                        }
                    }
                    if (columnName == "Planname")
                    {
                        if (string.IsNullOrEmpty(Planname))
                        {
                            result = "Please enter a plan name";
                        }
                        else if (Regex.IsMatch(Planname, name))
                        {
                            result = null;
                        }
                        else result = "Please enter a valid plan name";
                    }
                    if (columnName == "Password")
                    {
                        if (string.IsNullOrEmpty(Password))
                        {
                            result = "Password cannot be empty";
                        }
                        else if (IsAlphaNumeric(Password) == true)
                        {
                            result = null;
                        }
                    }
                    if (columnName == "Repassword")
                    {
                        if (String.IsNullOrEmpty(Repassword))
                        {
                            result = "Please re-type the passwords";
                        }
                        else if (Repassword.Equals(Password))
                        {
                            result = null;
                        }
                        else result = "Please check the passwords";
                    }
                    if (columnName == "Pax")
                    {
                        if (String.IsNullOrEmpty(Pax))
                        {
                            result = "Please enter Pax";
                        }
                        else if (IsNumeric(Pax) == true)
                        {
                            result = null;
                        }
                        else result = "Pax must contain only digits";
                    }
                    if (columnName == "Date")
                    {
                        if (String.IsNullOrEmpty(Date))
                        {
                            result = "Please select date";
                        }
                        else if (!(Date == ""))
                        {
                            result = null;
                        }
                    }
                   if (columnName == "ToDate")
                    {
                        if (String.IsNullOrEmpty(ToDate))
                        {
                            result = "Please select date";
                        }
                        else if (!(ToDate == ""))
                        {
                            result = null;
                        }
                    }
                    if (columnName == "Rooms")
                    {
                        if (String.IsNullOrEmpty(Rooms))
                        {
                            result = "Please enter Room Number";
                        }
                        else if (IsNumeric(Rooms) && Rooms.Length == 3)
                        {
                            result = null;
                        }
                        else result = "Room number must contain only 3 digits";
                    }
                    if (columnName == "Transferroom")
                    {
                        if (String.IsNullOrEmpty(Transferroom))
                        {
                            result = "Please enter Room Number";
                        }
                        else if (IsNumeric(Transferroom) && Transferroom.Length == 3)
                        {
                            result = null;
                        }
                        else result = "Room number must contain only 3 digits";
                    }
                    if (columnName == "Roomname")
                    {
                        //  string reg = @"^[A-Za-z0-9- ]+$";
                        if (string.IsNullOrEmpty(Roomname))
                        {
                            result = "Please enter a room name";
                        }
                        else if (IsAlphaNumeric(Roomname) == true)
                        {
                            result = null;
                        }
                        else result = "Alphanumaric";
                    }
                    if (columnName == "Number")
                    {
                        if (string.IsNullOrEmpty(Number))
                        {
                            result = "Enter the Mobile number";
                        }
                        else if (IsNumeric(Number) == true && Number.Length == 10)
                        {
                            result = null;
                        }
                        else result = "Check The Mobile Number";
                    }
                    if (columnName == "Number1")
                    {
                        if (string.IsNullOrEmpty(Number1))
                        {
                            result = "Please enter a number";
                        }
                        else if (IsNumeric(Number1) == true)
                        {
                            result = null;
                        }
                        else result = "Enter only numbers";
                    }
                    if (columnName == "Paxadult")
                    {
                        if (string.IsNullOrEmpty(Paxadult))
                        {
                            result = "Please enter Adult";
                        }
                        else if (IsNumeric(Paxadult) == true && Paxadult.Length <= 2)
                        {
                            result = null;
                        }
                        else result = "Enter only numbers";
                    }
                    if (columnName == "Paxchild")
                    {
                        if (IsNumeric(Paxchild) == true && Paxchild.Length <= 2)
                        {
                            result = null;
                        }
                        else result = "Enter only numbers";
                    }
                    if (columnName == "Taxcode")
                    {
                        if (IsAlphaNumeric(Taxcode) == true)
                        {
                            result = null;
                        }
                        else result = "Please enter a valid code";
                    }
                    if (columnName == "Toamount")
                    {
                        string reg = @"(\D)\s*([.\d,]+)";
                        if (string.IsNullOrEmpty(Toamount))
                        {
                            result = "Please enter valid amount";
                        }
                        else if (Regex.IsMatch(Toamount, reg) && IsAlphaNumeric(Toamount) == false)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Amount Must be like 1000.00";
                        }
                    }
                    if (columnName == "Website")
                    {
                        string web = @"^([\w\-]+)(\.[\w\-]+)((\.(\w){2,3})+)$";

                        if (Regex.IsMatch(Website, web))
                        {
                            result = null;
                        }
                        else { result = "please enter valid website (example xyz.com) "; }
                    }
                    if (columnName == "Transactionno")
                    {
                        if (string.IsNullOrEmpty(Transactionno))
                        {
                            result = "Transaction Number cannot be empty";
                        }
                        else if (IsAlphaNumeric(Transactionno) == true && Transactionno.Length == 12)
                        {
                            result = null;
                        }
                        else result = "Please check the transaction no";
                    }
                    if (columnName == "Chequeno")
                    {
                        if (string.IsNullOrEmpty(Chequeno))
                        {
                            result = "Cheque number cannot be empty";
                        }
                        else if (IsNumeric(Chequeno) == true && Chequeno.Length == 6)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please provide a valid cheque no";
                        }
                    }
                    if (columnName == "Extra")
                    {
                        if (IsNumeric(Extra) == true && Extra.Length <= 2)
                        {
                            result = null;
                        }
                        else result = "Enter only numbers";
                    }
                    if (columnName == "Extraadult")
                    {
                        if (IsNumeric(Extraadult) == true && Extraadult.Length <= 2)
                        {
                            result = null;
                        }
                        else result = "Enter only numbers";
                    }
                    if (columnName == "Extrachild")
                    {
                        if (IsNumeric(Extrachild) == true && Extrachild.Length <= 2)
                        {
                            result = null;
                        }
                        else result = "Enter only numbers";
                    }
                    if (columnName == "Tipamount")
                    {
                        if (Regex.IsMatch(Tipamount, reg) && IsAlphaNumeric(Tipamount) == false)
                        {
                            result = null;
                        }
                        else result = "Amount must be 1000.00";
                    }
                    if (columnName == "Authorization")
                    {
                        if (string.IsNullOrEmpty(Authorization))
                        {
                            result = "Authorization cannot be empty";
                        }
                        else if (Regex.IsMatch(Authorization, name))
                        {
                            result = null;
                        }
                        else result = "Please enter only alphabets";
                    }
                    if (columnName == "Reservationid")
                    {
                        if (string.IsNullOrEmpty(Reservationid))
                        {
                            result = "Reservation ID cannot be null";
                        }
                        else if (IsNumeric(Reservationid) == true)
                        {
                            result = null;
                        }
                        else result = "Please enter only numbers";
                    }
                    if (columnName == "Special")
                    {
                        if (IsAlphaNumeric(Special) == true)
                        {
                            result = null;
                        }
                        else result = "Please enter a valid Instruction";
                    }
                    if (columnName == "Addharcard")
                    {
                        if (string.IsNullOrEmpty(Addharcard))
                        {
                            result = "Aadhar number cannot be empty";
                        }
                        else if (Regex.IsMatch(Addharcard, aadhar) && Addharcard.Length == 14)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Invalid aadhar number";
                        }
                    }
                    if (columnName == "Pancard")
                    {
                        if (string.IsNullOrEmpty(Pancard))
                        {
                            result = "Pan number cannot be empty";
                        }
                        else if (Regex.IsMatch(Pancard, pan) && Pancard.Length == 10)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Invalid pan number";
                        }
                    }
                    if (columnName == "Driving")
                    {
                        if (string.IsNullOrEmpty(Driving))
                        {
                            result = "Driving license cannot be empty";
                        }
                        else if (Regex.IsMatch(Driving, drive) && Driving.Length == 16)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Invalid Driving license number";
                        }
                    }
                    if (columnName == "Passport")
                    {
                        if (string.IsNullOrEmpty(Passport))
                        {
                            result = "Passport number cannot be empty";
                        }
                        else if (Regex.IsMatch(Passport, passport) && Passport.Length == 9)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Invalid passport number";
                        }
                    }
                    if (columnName == "Voterid")
                    {
                        if (string.IsNullOrEmpty(Voterid))
                        {
                            result = "Voterid number cannot be empty";
                        }
                        else if (Regex.IsMatch(Voterid, voter) && Voterid.Length == 9)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Invalid voterid number";
                        }
                    }
                    if (columnName == "ItemName")
                    {
                        if (string.IsNullOrEmpty(ItemName))
                        {
                            result = "ItemName Should not be empty";
                        }
                        else if (Regex.IsMatch(ItemName, alphanumwithspace))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only Alphabets";
                        }
                    }
                    if (columnName == "ItemPrice")
                    {
                        if (string.IsNullOrEmpty(ItemPrice))
                        {
                            result = "Price Should not be empty";
                        }
                        else if (Regex.IsMatch(ItemPrice, percentage))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Price should be 0.00";
                        }
                    }
                    if (columnName == "ItemDetails")
                    {
                        if (string.IsNullOrEmpty(ItemPrice))
                        {
                            result = "Details Should not be empty";
                        }
                        //else if (Regex.IsMatch(ItemDetails, alphanumwithspace))
                        //{
                        //    result = null;
                        //}
                        else
                        {
                            result = null;
                        }
                    }
                    if (columnName == "ReportingName")
                    {
                        if (string.IsNullOrEmpty(ReportingName))
                        {
                            result = "Reporting Name Should not be empty";
                        }
                        else if (Regex.IsMatch(ReportingName, name))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only Alphabets";
                        }
                    }
                    if (columnName == "ItemDescription")
                    {
                        if (string.IsNullOrEmpty(ItemDescription))
                        {
                            result = "Description Should not be empty";
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    if (columnName == "UserName")
                    {
                        if (string.IsNullOrEmpty(UserName))
                        {
                            result = "Username Should not be empty";
                        }
                        else if (Regex.IsMatch(UserName, alphanum))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Don't Use Special Characters";
                        }
                    }
                    if (columnName == "CustomerPhone")
                    {
                        if (string.IsNullOrEmpty(CustomerPhone))
                        {
                            result = "Mobile Number Should not be empty";
                        }
                        else if (IsNumeric(CustomerPhone) == true && CustomerPhone.Length == 10)
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please verify mobile number";
                        }
                    }
                    if (columnName == "CustomerName")
                    {
                        if (string.IsNullOrEmpty(CustomerName))
                        {
                            result = "Name Should not be empty";
                        }
                        else if (Regex.IsMatch(CustomerName, alphanumwithspace))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Enter only Alphabets";
                        }
                    }
                    if (columnName == "CustomerEmail")
                    {
                        string mail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                        if (string.IsNullOrEmpty(CustomerEmail))
                        {
                            result = null;
                        }
                        else if (Regex.IsMatch(CustomerEmail, mail))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Please check the email address ";
                        }
                    }
                    //if (columnName == "QuantityZero")
                    //{
                    //    if (string.IsNullOrEmpty(QuantityZero))
                    //    {
                    //        result = "Enter Quantity";
                    //    }
                    //    else if (IsNumeric(QuantityZero) == true)
                    //    {
                    //        result = null;
                    //    }
                    //    else if (QuantityZero == "0")
                    //    {
                    //        result = "Quantity Should not be zero";
                    //    }
                    //    else result = "Enter Numbers Only";
                    //}
                    //if (columnName == "Quantityone")
                    //{
                    //    if (string.IsNullOrEmpty(Quantityone))
                    //    {
                    //        result = "Enter Quantity";
                    //    }
                    //    else if (IsNumeric(Quantityone) == true)
                    //    {
                    //        result = null;
                    //    }
                    //    else if (Quantityone == "0")
                    //    {
                    //        result = "Quantity Should not be zero";
                    //    }
                    //    else result = "Enter Numbers Only";
                    //}
                }
                return result;
            }
        }
    }
}
