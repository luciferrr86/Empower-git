using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class EmployeeAttendenceViewModel
    {

        public int Id { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public bool IsApproved { get; set; }
        public int? LeaveType { get; set; }

        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsEdit { get; set; }
        //  public string Duration { get; set; }

        public string DateView
        {
            get
            {
                string dt = string.Empty;
                if (Date.HasValue)
                {
                    dt = String.Format("{0:M/d/yyyy}", Date.Value);
                }
                return dt;
            }
        }

        
        public string Duration
        {
            get
            {
               string durat = string.Empty;
                if (PunchIn != null && PunchOut!= null && PunchIn != "00:00:00" && PunchOut != "00:00:00")
                {
                    
                    var duration = DateTime.Parse(PunchOut).Subtract(DateTime.Parse(PunchIn));                   
                    if(duration.Hours < 0)
                    {
                        duration = DateTime.Parse(PunchOut).AddDays(1).Subtract(DateTime.Parse(PunchIn));
                        // duration.Hours = duration.Hours + 24;
                    }
                    durat = duration.ToString();
                }
                else if(!(PunchIn == null && PunchOut == null))
                {
                    durat = "<span style='background-color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;</Span>";
                }
               
                return durat;
            }
        }

        public int LeaveStatus
        {
            get
            {
                int leaveStatus;
                if (PunchIn != null && PunchOut != null && PunchIn != "00:00:00" && PunchOut != "00:00:00")
                {

                    var duration = DateTime.Parse(PunchOut).Subtract(DateTime.Parse(PunchIn));
                    if (duration.Hours < 0)
                    {
                        duration = DateTime.Parse(PunchOut).AddDays(1).Subtract(DateTime.Parse(PunchIn));
                        // duration.Hours = duration.Hours + 24;
                    }
                    if (duration.Hours > 9)
                    {
                        leaveStatus = (int)LeaveTypes.FullDay;
                    }
                    else if (duration.Hours > 7)
                    {
                        leaveStatus = (int)LeaveTypes.ShortLeave;
                    }
                    else if (duration.Hours > 4)
                    {
                        leaveStatus = (int)LeaveTypes.HalfDay;
                    }
                    else 
                    {
                        leaveStatus = (int)LeaveTypes.UnpaidLeave;
                    }
                }
                else
                {
                    leaveStatus = (int)LeaveTypes.NotApplicable;
                }

                return leaveStatus;
            }
        }


    }


}
