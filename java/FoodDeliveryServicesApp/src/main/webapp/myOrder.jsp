<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="form"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>


<table>
	<tr>
		<td><a href="<c:url value='/myaccount' />">My profile</a><br />
			<sec:authorize access="hasRole('ROLE_CONSUMER')">
				<a href="<c:url value='/myorder' />">Orders</a>
				<br />
				<a href="<c:url value='/orderhistory' />">Order History</a>
				<br />
			</sec:authorize></td>
		<td><b><u>Arriving (On the way)</u></b> <br /> <br /> <c:forEach
				items="${orders}" var="order" varStatus="orderCounter">
				<div class="divBorder" id="rowOrder">
					<div class="divTitle">
						<b>Order#${order.id} <br/>
						Order Date <fmt:formatDate type="both" value="${order.orderDate}" />
						<span style="float:right">Total $${order.getTotalPrice() }</span></b>
					</div>
					<table>
						<tr>
							<td>Order</td>
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
				</div>
				<br/>
			</c:forEach>
			</td>

	</tr>

</table>
