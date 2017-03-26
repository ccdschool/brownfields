<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>

<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>

<link rel="stylesheet" href="<c:url value="/resources/css/index.css" />">

<div id="foodItemForm">

<h1>Add Food Item</h1>

	<form:form action="${editItem.id>0 ? '../add' : ''}"
		enctype="multipart/form-data" method="post" modelAttribute="foodItem">

		<input type="hidden" name="id" value="${editItem.id}" /> 
		<input type="hidden" name="imgUrl" value="${editItem.imgUrl}" /> 
			
		<table>
			<tr>
				<td>Food Item Name:</td>
				<td><form:input path="name" value="${editItem.name}" /></td>
				<td><form:errors path="name"></form:errors></td>
			</tr>
			<tr>
				<td>Description:</td>
				<td><form:input name="description" path="${editItem.description}" /></td>
				<td><form:errors path="description"></form:errors></td>
			</tr>

			<tr>
				<td>Price per Item:</td>
				<td><form:input path="price" value="${editItem.price}" /></td>
				<td><form:errors path="price"></form:errors></td>
			</tr>
			<tr>
				<td>File to upload:</td>
				<td><form:input type="file" path="file"/><br /></td>
			</tr>
			
			<tr>
				<td></td>
				<td>
					<img width="80px" height="80px" src="<c:url value="${editItem.imgUrl}" />" />
				</td>
			</tr>
			
			<tr>
				<td>Choose Category:</td>
				<td>
					<select name='categoryId'>
			<c:forEach items="${categories}" var="category">
				<option value="${category.id}">${category.name}</option>
			</c:forEach>
		</select><br/>

		</td>
		</tr>
		<tr>
				<td></td>
				<td>
				<button class="button" type="submit">${editItem.id>0 ? 'Update' : 'Add'}</button>
		</td>
		</tr>
		</table>
		
		
		
	</form:form>
	</div>
