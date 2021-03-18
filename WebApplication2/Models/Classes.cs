using System;
using System.Collections.Generic;

public class Claim {

   public int MemberID { get; set; } 
  
	public DateTime ClaimDate { get; set; }
	public decimal ClaimAmount { get; set;  }
	public string FormatDate {get { return ClaimDate.ToShortDateString(); } }
}

public class Members
{

	public int MemberID { get; set; }

	public DateTime EnrollmentDate { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public List<Claim> lstClaims { get; set; }
	public string FormatDate { get { return EnrollmentDate.ToShortDateString(); } }
}

