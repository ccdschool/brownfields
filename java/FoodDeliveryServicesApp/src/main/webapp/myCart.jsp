<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="form"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>

<h3>My cart</h3>
<c:if test="${sessionScope.order.orderLines!=null}">
	<table>
		<tr>
			<td>Order</td>
			<td>Item</td>
			<td>Price</td>
			<td>Quantity</td>
			<td>Total</td>
		</tr>
		<c:forEach items="${sessionScope.order.orderLines}" var="row"
			varStatus="rowCounter">
			<tr>
				<td>${rowCounter.count}</td>
				<td><c:out value="${row.foodItem.name }" /></td>
				<td><c:out value="${row.foodItem.price }" /></td>
				<form:form action="updateCart" modelAttribute="item" method="post">
					<td><form:input path="quantity" value="${row.quantity}"
							size="3" /></td>
					<td><c:out value="${row.quantity*row.foodItem.price}" /></td>
					<td><form:hidden path="foodItem.id" value="${row.foodItem.id}" />
						<input type="submit" value="Update"></td>
				</form:form>
				<td><form:form action="removeOrderLine" modelAttribute="item"
						method="post">
						<form:hidden path="foodItem.id" value="${row.foodItem.id}" />
						<input type="submit" id="remove" value="">
					</form:form></td>
			</tr>
		</c:forEach>
	</table>
	<h3>Total price : $${sessionScope.order.getTotalPrice()}</h3>
	<br/>
	<b><u>Shipping Address</u></b>
	<form:form modelAttribute="user" method="POST" action="placeOrder">
		<form:hidden path="id" />
		<form:hidden path="userType" />
		<form:hidden path="fullName" />
		<table>

			<tr>
				<td>Address</td>
				<td><form:input path="address" /></td>
				<td><form:errors path="address"></form:errors></td>
			</tr>
			<tr>
				<td>Email</td>
				<td><form:input path="email" /></td>
				<td><form:errors path="email"></form:errors></td>
			</tr>
			<tr>
				<td>Contact</td>
				<td><form:input path="contact" /></td>
				<td><form:errors path="contact"></form:errors></td>
			</tr>
			<tr>
				<td><input type="submit" value="Place Order" /></td>
			</tr>

		</table>
	</form:form>
</c:if>


