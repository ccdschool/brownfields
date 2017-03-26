<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!-- <a href="supplier/deliveries"><button type="submit">View
		Deliveries</button></a>
<a href="supplier/manageFoodItem"><button type="submit">Manage
		Food Items</button></a>
<a href="#"><button type="submit">Make offer</button></a> -->
<nav id="primary_nav_wrap">
	<ul>
		<li class="current-menu-item"><a href="<c:url value="/supplier" />">Home</a></li>
		<li><a href="<c:url value="/supplier/deliveries" />">View Deliveries</a></li>
		<li><a href="<c:url value="/supplier/manageFoodItem" />">Manage Food Items</a>
			<ul>
				<li><a href="<c:url value="/supplier/manageFoodItem/add" />">Add Food Item</a></li>
				
			</ul>
		</li>
		<li><a href="<c:url value="/supplier/deliveries" />">Make offer</a></li>
		<li><a href="#">Contact Us</a></li>
	</ul>
</nav>
<br />
<br />
<hr />