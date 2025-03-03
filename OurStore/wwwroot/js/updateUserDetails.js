
//const load = addEventListener("load", async () => {
//    try {
//        const id = sessionStorage.getItem("currentUser")
//        console.log(id)
//        const responseGet = await fetch(`api/users/${id}`, {
//            method: "GET",
//            headers: {
//                "Content-Type": "application/json"
//            } 
//        })
        
//        if (responseGet.ok) {
//            detailsUser = await responseGet.json()
//        }
//        document.querySelector("#emailUpdate").value = detailsUser.email
//        document.querySelector("#firstNameUpdate").value = detailsUser.firstName
//        document.querySelector("#lastNameUpdate").value = detailsUser.lastName
//        document.querySelector("#password").value = detailsUser.password
//    }
//    catch (err) {
//        alert(err)
//    }
//})

//const GetDataFromUpdate = () => {
//    const email = document.querySelector("#emailUpdate").value;
//    const firstName = document.querySelector("#firstNameUpdate").value;
//    const lastName = document.querySelector("#lastNameUpdate").value;
//    const password = document.querySelector("#password").value;
//    return { password, lastName, firstName, email }
//}
//const updateDetails = async () => {
//    const newUser = GetDataFromUpdate()
//    if (!newUser.password || !newUser.email)
//        return alert("password & email are required")
//    if (newUser.firstName.length > 15 || newUser.lastName.length > 15)
//        return alert("lastName & firstName need to be between 0 till 15")
//  /*  if(!CheckPassword(newUser.password))*/
//    try {
//        debugger
//        const id = sessionStorage.getItem("currentUser")
//        const responsePut = await fetch(`api/users/${id}`, {
//            method: "PUT",
//            headers: {
//                "Content-Type": "application/json"
//            },
//            body: JSON.stringify(newUser)
//        })
//        if (responsePut.ok)
//            alert(`updated succefully`)
//    }
//    catch (err) {
//        alert(err)
//    }
   
//    const CheckPassword = async () => {
//        const cPassword = document.querySelector("#password").value;
//        try {
//            const responsePost = await fetch(`api/users/password/?password=${cPassword}`, {
//                method: "POST",
//                headers: {
//                    "Content-Type": "application/json"
//                },
//                query: { password: cPassword }
//            });
//            const dataPost = await responsePost.json();
//            strengthMeter.value = dataPost;
//            resultPassword = dataPost
//        }
//        catch (err) {
//            alert(err)
//        }
//    }

// }
