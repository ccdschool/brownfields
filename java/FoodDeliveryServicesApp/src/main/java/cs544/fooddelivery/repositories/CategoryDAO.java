package cs544.fooddelivery.repositories;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import cs544.fooddelivery.domain.Category;
import cs544.fooddelivery.domain.Order;

@Repository
@Transactional(propagation=Propagation.REQUIRED)
public interface CategoryDAO extends JpaRepository<Category, Long>  {
	public List<Category> findAll();
}
