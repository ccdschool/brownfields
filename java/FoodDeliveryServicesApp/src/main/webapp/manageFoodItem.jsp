<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>

<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>

<link rel="stylesheet" href="<c:url value="/resources/css/index.css" />">
		
<div id="tblManageFoodItem">
		
<c:if test="${not empty foodItems}">
		<table border=1>
			<thead>
				<tr>
					<th>Food Item Name</th>
					<th>Image</th>
					<th>Price</th>
					<th>Description</th>
					<th>Food Category</th>
					<th colspan=2>Action</th>
				</tr>
			</thead>
			<tbody>
				<c:forEach items="${foodItems}" var="foodItem">
					<tr>
						<td><c:out value="${foodItem.name}" /></td>
						<td><img width="50px" height="50px" src="<c:url value="${foodItem.imgUrl}" />"/></td>
						<td><c:out value="${foodItem.price}" /></td>
						<td><c:out value="${foodItem.description}" /></td>
						<td><c:out value="${foodItem.category.name}" /></td>	
						<td><a href="<c:out value="manageFoodItem/edit/${foodItem.id}"/>">Edit</a></td>
						<td><a	href="<c:out value="manageFoodItem/delete/${foodItem.id}"/>">Delete</a></td>
					</tr>
				</c:forEach>
			</tbody>

		</table>
</c:if>

</div>
		
<c:if test="${empty foodItems}">
	<b>No Food item added</b>
</c:if>		
	