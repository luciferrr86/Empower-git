using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Enum
{
    public enum ModeOfInterviewEnum
    {
       SKype = 1,
       FaceToFace = 2,
       Telephoinc =3
    }
    public enum InterviewStatus
    {
        SKype = 1,
        FaceToFace = 2,
        Telephoinc = 3
    }

    public enum JobStatus
    {
        Applied = 1,
        Shortlisted = 2,
        Scheduled = 3,
        OnHold = 4,
        Hired = 5,
        Rejected = 6

    }
    enum CaseStudy
    {
        Empower360 = 0,
        DigiFit = 1,
        A4DataPro = 2,
        Excite = 3,
        HtmlEditor = 4
    }
}
