

namespace BlApi;
/// <summary>
/// The template of functions required from the cart-logical entity
/// </summary>
public interface ICart
    {
    public BO.Cart? Add(BO.Cart cart,int id);
    public BO.Cart? update(BO.Cart cart, int id,int num);
   public void OrderConfirmation(BO.Cart cart,string name, string email, string address);//
  //  public void OrderConfirmation(BO.Cart cart);
}
