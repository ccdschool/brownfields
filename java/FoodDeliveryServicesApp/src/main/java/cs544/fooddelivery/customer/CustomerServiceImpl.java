package cs544.fooddelivery.customer;

import java.util.List;

import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import cs544.fooddelivery.domain.Customer;
import cs544.fooddelivery.domain.FoodItem;
import cs544.fooddelivery.domain.OrderLine;
import cs544.fooddelivery.domain.Order;
import cs544.fooddelivery.repositories.CustomerDAO;

@Service("customerService")
@Transactional(propagation=Propagation.REQUIRED)
public class CustomerServiceImpl implements ICustomerService {
	
	@Autowired
	private CustomerDAO customerDAO;

	public Customer update(Customer customer) {
		customerDAO.save(customer);
		return customer;
	}

	public List<Order> orders() {
		// TODO Auto-generated method stub
		return null;
	}

	public List<Order> orderHistory() {
		// TODO Auto-generated method stub
		return null;
	}

	public Order addToCart(FoodItem item, HttpSession session) {
		boolean flag=true;
		Order cart = (Order) session.getAttribute("order");
		OrderLine newOrderLine = new OrderLine(item);
		if(cart!=null){
			if(cart.getSupplierId()!=item.getSupplier().getId()){
				throw new IllegalArgumentException("Incompatible item in cart.");
			}
			for (OrderLine orderLine : cart.getOrderLines()) {
				if(orderLine.getFoodItem().getId().equals(item.getId())){
					int quantity = orderLine.getQuantity()+1;
					orderLine.setQuantity(quantity);					
					flag=false;
					break;
				}
			}
			if(flag){
				newOrderLine.setQuantity(1);
				//newOrderLine.setOrder(cart);
				cart.addOrderLine(newOrderLine);
			}
		}else{
			cart = new Order();
			newOrderLine.setQuantity(1);
			//newOrderLine.setOrder(cart);
			cart.addOrderLine(newOrderLine);
			cart.setSupplierId(item.getSupplier().getId());
			
		}
		return cart;
	}

	public Order updateCart(OrderLine updatedOrderLine, HttpSession session) {
		
		Order cart = (Order) session.getAttribute("order");
		if(cart!=null){
			for (OrderLine orderLine : cart.getOrderLines()) {
				if(orderLine.getFoodItem().getId().equals(updatedOrderLine.getFoodItem().getId())){
					int quantity = updatedOrderLine.getQuantity();
					orderLine.setQuantity(quantity);					
					break;
				}
			}			
		}
		return cart;
	}

	public Order removeFromOrder(OrderLine removeOrderLine, HttpSession session) {
		Order cart = (Order) session.getAttribute("order");
		if(cart!=null){
			for (OrderLine orderLine : cart.getOrderLines()) {
				if(orderLine.getFoodItem().getId().equals(removeOrderLine.getFoodItem().getId())){
					cart.getOrderLines().remove(orderLine);				
					break;
				}
				
			}
		}
		return cart;
	}

	
}
