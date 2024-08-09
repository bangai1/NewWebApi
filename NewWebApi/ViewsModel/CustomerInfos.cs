using NewWebApi.Models;


namespace NewWebApi.ViewsModel;

public class CustomerInfos
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public List<Customer>? customers { get; set; }
    public List<Bills>? bills { get; set; }
    public List<Company>? companies { get; set; }

}
