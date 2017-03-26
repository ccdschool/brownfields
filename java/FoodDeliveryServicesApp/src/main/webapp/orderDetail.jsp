<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="form"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>

<link rel="stylesheet" href="<c:url value="/resources/css/index.css" />">


<div id="orderDetailTable">
<table>
	<thead>
		<tr>
			<th>Food Item Name</th>
			<th>Image</th>
			<th>Price</th>
			<th>Description</th>
			<th>Food Category</th>
		</tr>
	</thead>
	<tbody>
		<c:forEach items="${order.orderLines}" var="orderLine">
			<tr>
				<td><c:out value="${orderLine.foodItem.name}" /></td>
				<td><img width="80px" height="80px"
					src="<c:url value="${orderLine.foodItem.imgUrl}" />" /></td>
				<td><c:out value="${orderLine.foodItem.price}" /></td>
				<td><c:out value="${orderLine.foodItem.description}" /></td>
				<td><c:out value="${orderLine.foodItem.category.name}" /></td>
			</tr>
		</c:forEach>
	</tbody>

</table>

</div>

<div>
	<h3>Customer Information</h3>
	Name: <span>${order.customer.fullName }</span><br /> Email: <span>${order.customer.email }</span><br />
	Contact: <span>${order.customer.contact }</span><br /> Address: <span>${order.customer.address }</span><br />
</div>
