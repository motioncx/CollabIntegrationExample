namespace BelkSnowFlakeServiceProxies.Query;

public class QuerySnowFlakeRequestByOrderId
{
    // - storeNumber
    // - registerNumber
    // - transactionNumber
    // - cardNumber
    // - date
    // - firstName
    // - lastName
    
    // - fromDate
    // - toDate
    // - phoneNo
    // - emailId

    
    
    public int? storeNumber { get; set; }
    public int? registerNumber { get; set; }
    public int? transactionNumber { get; set; }
    public int? cardNumber { get; set; }
    
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? phoneNo { get; set; }
    public string? emailId { get; set; } 
    
    public string? date { get; set; }
    public string? fromDate { get; set; }
    public string? toDate { get; set; }
    
    public string Env { get; set; }

    public string GetOrderByPhoneNumber()
    {
        SetToDate();
        SetFromDate();
        var searchKey = $"?phoneNo={phoneNo}&fromDate={fromDate}&toDate={toDate}";
        return searchKey;
    }
    public string GetOrderByEmail()
    {
        SetToDate();
        SetFromDate();
        var searchKey = $"?emailId={emailId}&fromDate={fromDate}&toDate={toDate}";
        
        return searchKey;
    }
    public string GetOrderByCard()
    {
        SetToDate();
        SetFromDate();
        var searchKey = $"?storeId={IntegerAddPadding((int)storeNumber)}&cardNumber={IntegerAddPadding((int)cardNumber)}&firstName={firstName}&lastName={lastName}&fromDate={fromDate}&toDate={toDate}";
        return searchKey;
    }
    private void SetToDate()
    {
        if (toDate == "null")
        {
            toDate = "";
        }
        else if(toDate != null)
        {
            try
            {
                var tempDate = DateTime.Parse(toDate);
                toDate += ((DateTime)tempDate).ToString("MMddyyyy");
            }
            catch 
            {
                
            }
        }
    }

    private void SetFromDate()
    {
        if (fromDate == "null")
        {
            fromDate = "";
        }
        else if(fromDate != null)
        {
            try
            {
                var tempDate = DateTime.Parse(fromDate);
                fromDate += ((DateTime)tempDate).ToString("MMddyyyy");
            }
            catch 
            {
                
            }
        }
    }
    
    
    
    public string GetOrderIdConcatination()
    {
        string ret = "";
        ret += IntegerAddPadding((int)storeNumber);
        ret += IntegerAddPadding((int)registerNumber);
        ret += IntegerAddPadding((int)transactionNumber);
        if (date != "null")
        {
            try
            {
                var tempDate = DateTime.Parse(date);
                ret += ((DateTime)tempDate).ToString("MMddyyyy");
            }
            catch 
            {
                
            }
        }
        return ret;
    }

    public static string IntegerAddPadding(int val)
    {
        return val.ToString("D" + 4);
    }

    public enum SEARCH_BY
    {
        ORDER_SPECIFIED,
        CARD_SPECIFIED,
        PHONE_SPECIFIED,
        EMAIL_SPECIFIED,
        NOT_SPECIFIED
    }

    public SEARCH_BY GetWhatToQuery()
    {
        if (CheckIfOrderIdExists())
        {
            return SEARCH_BY.ORDER_SPECIFIED;
        }

        if (CheckIfCreditCardExists())
        {
            return SEARCH_BY.CARD_SPECIFIED;
        }

        if (CheckIfPhoneExists())
        {
            return SEARCH_BY.PHONE_SPECIFIED;
        }

        if (CheckIfEmailExists())
        {
            return SEARCH_BY.EMAIL_SPECIFIED;
        }

        return SEARCH_BY.NOT_SPECIFIED;
    }

    public bool CheckIfEmailExists()
    {
        if (!string.IsNullOrWhiteSpace(emailId))
        {
            return true;
        }

        return false;
    }
    
    public bool CheckIfPhoneExists()
    {
        if (!string.IsNullOrWhiteSpace(phoneNo))
        {
            return true;
        }

        return false;
    }
    public bool CheckIfCreditCardExists()
    {
        if (cardNumber != null && cardNumber > 0 &&
            !string.IsNullOrWhiteSpace(firstName) &&
            !string.IsNullOrWhiteSpace(lastName))
        {
            return true;
        }

        return false;
    }
    
    public bool CheckIfOrderIdExists()
    {
        if (storeNumber != null && storeNumber > 0 &&
            registerNumber != null && registerNumber > 0 &&
            transactionNumber != null && transactionNumber > 0 &&
            date != null )
        {
            return true;
        }
        return false;
           
        
    }
}