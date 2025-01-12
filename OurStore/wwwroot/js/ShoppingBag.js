const load = addEventListener("load", async () => {
    viewShoppingBagProducts(JSON.parse(sessionStorage.getItem("shoppingBag")))
 
})

const viewShoppingBagProducts = async (products) => {
    document.getElementById("itemCount").textContent = products.length;
    document.querySelector("tbody").innerHTML = ''
    let totalPrice = 0;
    for (let i = 0; i < products.length; i++) {
        totalPrice += products[i].price
        viewOneShoppingBagProduct(products[i])
    }
    document.getElementById("totalAmount").textContent = totalPrice + ' ₪'
}

const viewOneShoppingBagProduct = async (product) => {
    const template = document.getElementById("temp-row")
    let cloneProduct = template.content.cloneNode(true)
    cloneProduct.querySelector(".image").style.backgroundImage = `url(./Images/${product.image})`
    cloneProduct.querySelector(".itemName").textContent = product.productName
    cloneProduct.querySelector(".itemNumber").innerText = product.price + ' ₪'
    cloneProduct.querySelector(".expandoHeight").addEventListener('click', () => { deleteFromCart(product.productId) })
    document.querySelector("tbody").appendChild(cloneProduct)
}

const deleteFromCart = async (productId) => {
    let products = JSON.parse(sessionStorage.getItem("shoppingBag"))
    let i;
    for (i = 0; i < products.length; i++) {
        if (products[i].productId == productId)
            break;
    }
    products.splice(i,1)
    sessionStorage.setItem("shoppingBag", JSON.stringify(products))
    console.log(products)
    viewShoppingBagProducts(products)
}


const getOrderData = () => {
    const orderDate = "2025-01-12"
    console.log(orderDate);
    const userId = JSON.parse(sessionStorage.getItem("currentUser"))
    const products = JSON.parse(sessionStorage.getItem("shoppingBag"))
    let orderItems = []
    let orderSum = 0;
    for (let i = 0; i < products.length; i++) {
        orderSum += products[i].price
        orderItems.push({ productId: products[i].productId })
    }
    return { orderDate, orderSum, userId, orderItems }
}

const placeOrder = async () => {
    const order = getOrderData();
    try {
        const responsePost = await fetch('/api/orders', {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(order) 
        })
        const dataPost = await responsePost.json();
        if (!responsePost.ok)
            alert("failed to complete the order")
        else {
            alert(`order ${dataPost.orderId} completed`)
            sessionStorage.setItem("shoppingBag", JSON.stringify([]))
            viewShoppingBagProducts([])
        }
    }
    catch (error) {
        alert(error)
    }
}