<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<script
	src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>
<link rel="stylesheet" href="<c:url value="/resources/css/index.css" />">
<h2>Dashboard</h2>

<c:if test="${not empty orders}">
	<form id="supplierForm" action="supplier/makeDelivery" method="post">
		<button class="button" type="submit">Make Delivery</button>
		<table border=1>
			<tr>
				<td><input type="checkbox" id="selecctall" /> Select All</td>
				<td></td>
			</tr>
			<c:forEach items="${orders}" var="order">
				<tr>
					<td><input class="checkbox" type="checkbox" name="orderIds[]"
						value="${order.id}" /></td>
					<td style="background-color: AliceBlue;"><b><a
							href="<c:url value='/order/${order.id }' />">OrderId#${order.id}
								${order.customer.fullName}</a></b>

						<ul style="list-style: none">
							<li><c:forEach items="${order.orderLines}" var="orderline">

									<li>${orderline.foodItem.name}(${orderline.quantity} *
										$${orderline.foodItem.price})</li>

								</c:forEach></li>
						</ul>

						<div style="margin-top: 10px;">
							<b>Total Price:  $ ${order.getTotalPrice()}</b>
						</div></td>
				</tr>
			</c:forEach>
		</table>
	</form>
</c:if>
<c:if test="${empty orders}">
	<b>No Order Placed</b>
</c:if>
<script language="JavaScript">
	$(document).ready(function() {
		$("#selecctall").change(function() {
			$("input:checkbox").prop('checked', $(this).prop("checked"));
		});
	});
</script>
