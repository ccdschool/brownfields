package cs544.fooddelivery.repositories;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import cs544.fooddelivery.domain.FoodItem;

@Repository
@Transactional(propagation=Propagation.REQUIRED)
public interface FoodItemDAO extends JpaRepository<FoodItem, Long>  {
	
	public List<FoodItem> findFoodItemBySupplier_Id(Long supplierId);
	
}

//public interface ProjectDAO extends JpaRepository<Project, Long>{
//
//    Page<Project> findAll(Pageable pageable);
//
//    List<Project> findByProjectID(Integer projectID);
//
//}
