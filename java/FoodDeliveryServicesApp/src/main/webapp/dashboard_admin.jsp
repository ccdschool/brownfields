<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<link rel="stylesheet" href="../resources/css/supplier.css">
<link rel="stylesheet" href="<c:url value="/resources/css/index.css" />">
<title>Admin</title>
</head>
<body>
	<h3>Categories</h3>	 
	<table border="1" cellpadding="10" bordercolor="black">
   
    <tr>
        <th>Category Name</th>        
        
    </tr>
    <c:forEach   items="${categories}" var="category">
    <tr>
        <td>
            <c:out value="${category.name}" />
        </td>
       
        <td>
            <a href="<c:url value='/editCategoryPage/${category.id}' />" >Update</a>
        </td>
         <td>
           <a href="<c:url value='/deleteCategory/${category.id}' />" >Delete</a>
        </td>
        
    </tr>
    </c:forEach>
</table>	


 <a href="<c:url value='/addCategoryPage' />" >Add Category</a>

</body>
</html>