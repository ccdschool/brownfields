<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>

<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Add/Edit Category</title>
</head>
<body>
	<%@ include file="header.jsp" %>
	<center>
	<h1>Add Category</h1>
	<form:form action="/FoodDeliverySystem/addCategory" method="post" modelAttribute="categoryAdd">
		Category Name: <input type="text" name="categoryName"   /><br />
		<button type="submit">Add</button>
	</form:form>
	</center>
</body>
</html>