const GetData = () => {
    const nameSearch = document.querySelector("#nameSearch").value;
    const minPrice = document.querySelector("#minPrice").value;
    const maxPrice = document.querySelector("#maxPrice").value;
    const categoryIds=[]
    return { nameSearch, minPrice, maxPrice, categoryIds }
}
const filterProducts = async () => {
    const { nameSearch, minPrice, maxPrice, categoryIds }= GetData()
    let url = "api/products/"
    if (minPrice || maxPrice || nameSearch || categoryIds)
        url += '?'
    if (nameSearch != '')
        url += `&desc=${nameSearch}`
    if (minPrice)
        url += `&minPrice=${minPrice}`
    if (maxPrice)
        url += `&maxPrice=${maxPrice}`
    if (categoryIds != '')
        url += `&categoryIds=${categoryIds}`
    try {
        const responseGet = await fetch(url, {
            method: 'GET',
            headers: {
                "Content-Type": "application/json"
            },
            query: {
                desc: nameSearch,
                minPrice: minPrice,
                maxPrice: maxPrice,
                categoryIds: categoryIds
            }
        })
        if (responseGet.ok) {
            const products = await responseGet.json();
            console.log(products)
            for (let i = 1; i < products.length; i++) {
                viewProducts(Products[i])
            }
        }
        else 
            alert("bad request")
    }
    catch (error) {
        alert(error)
    }
    const viewProducts = async (product) => {
        const template = document.getElementById("temp-card")
        let cloneProduct = template.content.cloneNode(true)
        cloneProduct.querySelector(".img-w").src="../Images/"+product.image
        cloneProduct.querySelector("h1").textContent = product.productName
        cloneProduct.querySelector(".price").innerText = product.price
        cloneProduct.querySelector(".description").innerText = product.description
        document.getElementById("PoductList").appendChild(cloneProduct)
    }
}