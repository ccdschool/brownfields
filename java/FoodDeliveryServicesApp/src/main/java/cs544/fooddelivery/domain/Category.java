package cs544.fooddelivery.domain;

import java.util.List;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.OneToMany;

@Entity
public class Category {
	
	@Id @GeneratedValue
	private Long id;
	private String name;
	
	@OneToMany(mappedBy="category")
	private List<FoodItem> foodItems;

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id=id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public List<FoodItem> getFoodItems() {
		return foodItems;
	}

	public void setFoodItems(List<FoodItem> foodItems) {
		this.foodItems = foodItems;
	}
}