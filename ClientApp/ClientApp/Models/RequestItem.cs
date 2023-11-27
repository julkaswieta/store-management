namespace ClientApp.Models.Finance;

public class RequestItem
{
    public RequestItem(FinancialRequest request)
    {
        Request = request;
        RequestText = request.ToString();
        if (Request.Status.Equals("Approved"))
            ButtonText = "Reject";
        else
            ButtonText = "Approve";
    }

    public string? RequestText { get; set; }
    public FinancialRequest Request { get; set; }
    public string ButtonText { get; set; }
}
