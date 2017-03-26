package cs544.fooddelivery.supplier;

import java.util.Date;
import java.util.List;

//package cs544.fooddelivery.usermgmt;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import cs544.fooddelivery.domain.Category;
import cs544.fooddelivery.domain.Delivery;
import cs544.fooddelivery.domain.FoodItem;
import cs544.fooddelivery.domain.Status;
import cs544.fooddelivery.domain.User;
import cs544.fooddelivery.order.OrderService;
import cs544.fooddelivery.repositories.CategoryDAO;
import cs544.fooddelivery.repositories.DeliveryDAO;
import cs544.fooddelivery.repositories.FoodItemDAO;
import cs544.fooddelivery.repositories.UserDAO;

@Service
@Transactional(propagation=Propagation.REQUIRED)
public class SupplierService {
	
	@Autowired
	private FoodItemDAO foodItemDAO;
	
	@Autowired
	private CategoryDAO categoryDAO;
	
	@Autowired
	private DeliveryDAO deliveryDAO;
	
	@Autowired
	private OrderService orderService;
	
	@Autowired
	private UserDAO userDAO;
	
	public Category getCategoryWithCategoryId(Long categoryId){
		return this.categoryDAO.findOne(categoryId);
	}
	
	public List<Category> getAllCategories(){
		return this.categoryDAO.findAll();
	}
	
	public List<User> getAllSuppliers(){
		return userDAO.findAllSuppliers();
	}
	
	public List<FoodItem> getAllFoodItemBySupplier_Id(long supplierId){
		return this.foodItemDAO.findFoodItemBySupplier_Id(supplierId);
	}
	
	public List<FoodItem> getAllFoodItems(){
		return this.foodItemDAO.findAll();
	}
	
	public void deleteFoodItemForId(Long foodItemId){
		this.foodItemDAO.delete(foodItemId);
	}
	
	public FoodItem getFoodItemForId(Long foodItemId){
		return this.foodItemDAO.findOne(foodItemId);
	}
	
	public void saveFoodItem(FoodItem fitem){
		this.foodItemDAO.save(fitem);
	}

	public void setFoodItemDAO(FoodItemDAO foodItemDAO) {
		this.foodItemDAO = foodItemDAO;
	}
	
	public void setCategoryDAO(CategoryDAO categoryDAO) {
		this.categoryDAO = categoryDAO;
	}
	
	public void saveDelivery(Date startDate, String[] orderIds){
		Delivery delivery = new Delivery();
		delivery.setStartDateTime(new Date());
		delivery.setStatus(Status.PENDING);
		
		for(String orderId:orderIds){
			delivery.addOrder(orderService.getOrder(Long.parseLong(orderId)));
		}
		deliveryDAO.save(delivery);
	}

	public List<Delivery> getAllDeliveries(long supplierId){
		return deliveryDAO.findAllBySupplierId(supplierId);
	}
	
	public Delivery getDelivery(long deliveryId) {
		return deliveryDAO.getDelivery(deliveryId);
	}

	public void completeDelivery(long deliveryId, Date endDate, int distance) {
		Delivery delivery = getDelivery(deliveryId);
		delivery.setEndDateTime(new Date());
		delivery.setDistance(distance);
		delivery.setStatus(Status.COMPLETE);
		deliveryDAO.save(delivery);
	}
}
