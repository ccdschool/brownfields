package cs544.fooddelivery.domain;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

@Entity
public class OrderLine {
	@Id
	@GeneratedValue
	private Long id;
	private int quantity;
	
	@ManyToOne
	private Order order;
	
	@ManyToOne
	private FoodItem foodItem;
	
	public OrderLine() {
		
	}
	
	public OrderLine(FoodItem item) {
		this.foodItem = item;
	}

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public int getQuantity() {
		return quantity;
	}

	public void setQuantity(int quantity) {
		this.quantity = quantity;
	}

	public Order getOrder() {
		return order;
	}

	public void setOrder(Order order) {
		this.order = order;
	}

	public FoodItem getFoodItem() {
		return foodItem;
	}

	public void setFoodItem(FoodItem foodItem) {
		this.foodItem = foodItem;
	}

	
	
//	methods
	public double getTotalPrice(){
		return this.getFoodItem().getPrice()*this.quantity;
	}
}