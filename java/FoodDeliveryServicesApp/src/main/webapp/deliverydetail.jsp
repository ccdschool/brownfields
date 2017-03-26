<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>

<link rel="stylesheet" href="<c:url value="/resources/css/index.css" />">

<h2>Delivery Items detail</h2>


<c:if test="${delivery.status != 'COMPLETE'}">
	<form method="post">
		Enter Miles Travelled: <input id="txtMiles" type="text"
			name="distance" />
		<button type="submit">Complete Delivery</button>
	</form>
	<br />
	<br />
</c:if>
	

<b>Start date: <fmt:formatDate type="both"
		value="${delivery.startDateTime}" /> <c:if
		test="${delivery.status == 'COMPLETE'}">
		<div>
			Delivered on :
			<fmt:formatDate type="both" value="${delivery.endDateTime}" />
		</div>
	</c:if>
</b>
<br />
<b><span> ${delivery.status}</span></b>
<br />

<c:forEach items="${delivery.orders}" var="order">
	<div>Price : ${order.getTotalPrice()}</div>
	<div>Ordered by (${order.customer.fullName})</div>
	<div>Address: ${order.customer.address}</div>

	<br />

	<table>
		<tr>
			<td></td>
			<td>Item</td>
			<td>Price</td>
			<td>Quantity</td>
			<td>Total</td>
		</tr>

		<c:forEach items="${order.orderLines}" var="row"
			varStatus="rowCounter">
			<tr>
				<td>${rowCounter.count}</td>
				<td><c:out value="${row.foodItem.name }" /></td>
				<td><c:out value="${row.foodItem.price }" /></td>
				<td><c:out value="${row.quantity}" /></td>
				<td><c:out value="${row.quantity*row.foodItem.price}" /></td>
			</tr>
		</c:forEach>
	</table>
</c:forEach>

