const categoryIds = []
const GetData = () => {
    document.getElementById("PoductList").innerHTML=""
    const nameSearch = document.querySelector("#nameSearch").value;
    const minPrice = document.querySelector("#minPrice").value;
    const maxPrice = document.querySelector("#maxPrice").value;
    return { nameSearch, minPrice, maxPrice, categoryIds }
}
const load = addEventListener("load", async () => {
    filterProducts()
    getCategories()
})
const viewProducts = async (products) => {
    for (let i = 0; i < products.length; i++) {
        viewOneProduct(products[i])
    }
}

const viewOneProduct = async (product) => {
    const template = document.getElementById("temp-card")
    let cloneProduct = template.content.cloneNode(true)
    cloneProduct.querySelector("img").src =`../Images/${product.image}`
    cloneProduct.querySelector("h1").textContent = product.productName
    cloneProduct.querySelector(".price").innerText = product.price
    cloneProduct.querySelector(".description").innerText = product.description
    document.getElementById("PoductList").appendChild(cloneProduct)
}
const filterProducts = async () => {
    const { nameSearch, minPrice, maxPrice, categoryIds } = GetData()
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
            viewProducts(products)
        }
        else 
            alert("bad request")
    }
    catch (error) {
        alert(error)
    }
}
const viewCategories = async (categories) => {
    for (let i = 0; i < categories.length; i++) {
        viewOneCategory(categories[i])
    }
}
const viewOneCategory = async (category) => {
    let tempCategory = document.getElementById("temp-category");
    let clonecategory = tempCategory.content.cloneNode(true);
    clonecategory.querySelector(".OptionName").innerText = category.categoryName;
    clonecategory.querySelector(".opt").addEventListener('change', () => { })
    document.getElementById("categoryList").appendChild(clonecategory)
}
const getCategories = async () => {
    try {
        const responseGet = await fetch('/api/category', {
            method: 'GET',
            headers: {
                "Content-Type": "application/json"
            }
        })
        if (responseGet.ok) {
            const categories = await responseGet.json();
            viewCategories(categories)
        }
        else
            alert("bad request")
    }
    catch (error) {
        alert(error)
    }
}
