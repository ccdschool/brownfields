package cs544.fooddelivery.order;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import cs544.fooddelivery.domain.Order;
import cs544.fooddelivery.repositories.OrderDAO;

@Service
@Transactional(propagation=Propagation.REQUIRED)
public class OrderService {
	
	@Autowired
	private OrderDAO orderDAO;
	
	public void setOrderDAO(OrderDAO orderDAO){
		this.orderDAO=orderDAO;
	}
	
	public List<Order> getAllRequestedOrderForSupplierId(Long supplierId){
		return this.orderDAO.findAllPendingOrdersForSupplier(supplierId);
	}
	
	public Order getOrder(Long orderId){
		return this.orderDAO.findOne(orderId);
	}	
	
	public void save(Order order){
		orderDAO.save(order);
	}
	
	public List<Order> getAllPendingOrderOfCustomer(Long customerId){
		return orderDAO.findAllPendingOrdersForCustomer(customerId);
	}
	public List<Order> getAllDeliveredOrderOfCustomer(Long customerId){
		return orderDAO.findAllDeliveredOrdersForCustomer(customerId);
	}
}
