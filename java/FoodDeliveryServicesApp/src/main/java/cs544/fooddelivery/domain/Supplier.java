package cs544.fooddelivery.domain;

import java.util.List;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.OneToMany;

@Entity
@DiscriminatorValue("ROLE_SUPPLIER")
public class Supplier extends User {
	private double deliveryRadius;
	
	@OneToMany(mappedBy="supplier")
	private List<FoodItem> foodItems;
	
	public Supplier(){
		super();
	}
	
	public Supplier(String userName, String password, String fullName, String address, String email, String contact,
			boolean isActive, double deliveryRadius) {
		super(userName, password, fullName, address, email, contact, isActive);
		this.deliveryRadius = deliveryRadius;
	}

	public double getDeliveryRadius() {
		return deliveryRadius;
	}

	public void setDeliveryRadius(double deliveryRadius) {
		this.deliveryRadius = deliveryRadius;
	}
}
