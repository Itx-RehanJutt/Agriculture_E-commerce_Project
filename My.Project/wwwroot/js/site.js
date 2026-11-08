$(document).ready(function () {

    $(window).scroll(function () {
        var navbar = $('.navbar-custom');
        if ($(window).scrollTop() > 40)
        {
            navbar.addClass('scrolled');
        }
        else
        {
            navbar.removeClass('scrolled');
        }
    });

    try {
        $(".product").on("click", function () {
            const id = $(this).data("id");
            window.location.href = `/Home/ProductDetail/${id}`;
        });
    } catch (error) {
        console.error("Error caught:", error);
    }

    $("#increaseQuantity").on("click" ,function () {
        let qty = parseInt($("#quantity").val());
        $("#quantity").val(qty + 1);
    });

    $("#decreaseQuantity").on("click" , function () {
        let qty = parseInt($("#quantity").val());
        if (qty > 1) {
            $("#quantity").val(qty - 1);
        }
    });

    // Select All functionality
    $(document).on("change", ".selectAll", function () {
        const isChecked = $(this).is(":checked");
        $(".product-checkbox").prop("checked", isChecked); // Tick all product checkboxes

        const selectedProductIds = $(".product-checkbox:checked").map(function () {
            return $(this).val(); // Collect product IDs of all selected checkboxes
        }).get();

        // Update data for all selected products
        updateCartData(selectedProductIds);
    });

    // Individual checkbox functionality
    $(document).on("change", ".product-checkbox", function () {
        const selectedProductIds = $(".product-checkbox:checked").map(function () {
            return $(this).val();
        }).get();

        updateCartData(selectedProductIds);
    });

    //$(document).on("click", ".updatequantity", function () {
    //    const productId = $(this).data("product-id");
    //    const change = $(this).data("change");
    //    // Check if the corresponding checkbox is selected
    //    const $checkbox = $(`.product-checkbox[value='${productId}']`);
    //    if (!$checkbox.is(":checked")) {
    //        alert("Please select the product first.");
    //        return;
    //    }
    //    // Get the quantity input field
    //    const $quantityInput = $(`#quantity-${productId}`);
    //    const currentQuantity = parseInt($quantityInput.val());

    //    // Calculate new quantity
    //    const newQuantity = currentQuantity + change;

    //    // Ensure quantity is valid
    //    if (newQuantity < 1) {
    //        alert("Quantity must be at least 1.");
    //        return;
    //    }

    //    // AJAX request to update quantity
    //    $.ajax({
    //        url: '/Cart/UpdateQuantity',
    //        method: 'POST',
    //        contentType: 'application/json',
    //        data: JSON.stringify({
    //            productId: productId,
    //            quantity: newQuantity
    //        }),

    //        success: function (response) {
    //            $quantityInput.val(response.quantity);
    //            if (response.quantity == undefined || response.quantity == null) {
    //                $(".quantity").text(`Subtotal (0 items)`);
    //            }
    //            else {
    //                $('.quantity').text(`Subtotal (${response.quantity} items)`);
    //            }
    //            //$('.totalPrice').text(`Rs. ${response.totalPrice}`);
    //            $('.totalPrice').text(response.totalPrice);
    //        },
    //        error: function () {
    //            alert("Failed to update the quantity.");
    //        }
    //    });
    // });
    $(document).on("click", ".updatequantity", function () {
        let selectedProducts = [];
        let change = $(this).data("change");``

        $(".product-checkbox:checked").each(function () {
            let productId = $(this).val();
            let quantity = parseInt($(`#quantity-${productId}`).val());
            if (quantity < 1) {
                alert("Quantity must be at least 1.");
                return;
            }
            selectedProducts.push({
                productId: productId,
                quantity: quantity + change
            });
        });
        if (selectedProducts.length === 0) {
            alert("Please select at least one product.");
            return;
        }
            // Ensure quantity is valid
        $.ajax({
            url: '/Cart/UpdateMultipleCartItems',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ cartItems: selectedProducts }),
            success: function (response) {
                response.items.forEach(item => {
                    $(`#quantity-${item.productId}`).val(item.quantity);
                });
                updateCartCount(); // Cart count update karo

                $('.quantity').text(`Subtotal (${response.totalQuantity} items)`);
                $('.totalPrice').text(response.totalPrice);
            },
            error: function () {
                alert("Failed to update the quantity.");
            }
        });
    });

    // Function to update cart data
    function updateCartData(productIds) {
        $.ajax({
            url: '/Cart/GetCartData',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(productIds),
            success: function (response) {
                if (response.success) {
                    if (response.data.totalQuantity == undefined) {
                        $(".quantity").text(`Subtotal (0 items)`);
                    }
                    else {
                        $(".quantity").text(`Subtotal (${response.data.totalQuantity} items)`);
                    }
                    $(".totalPrice").text(`Rs. ${response.data.totalPrice}`);
                }
            },
            error: function () {
                alert("An error occurred while fetching cart data.");
            }
        });
    }
    // Update Cart Summary 
    $(document).on("change", ".product-checkbox", function () {
        const selectedProductIds = [];
        $(".product-checkbox:checked").each(function () {
            selectedProductIds.push($(this).val());
        });

        // AJAX request to update the summary
        $.ajax({
            url: '/Cart/GetCartData',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(selectedProductIds),
            success: function (response) {
                if (response.success) {
                    if (response.data.quantity == undefined) {
                        $(".quantity").text(`Subtotal (0 items)`);
                    }
                    else {
                        $('.quantity').text(`Subtotal (${response.data.quantity} items)`);
                    }
                    $('.totalPrice').text(`Rs. ${response.data.totalPrice}`);
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("An error occurred while fetching cart summary.");
            }
        });
    });

    // Delete 1 cart item
    $('.delete-cart-item').on('click', function () {
        var productId = $(this).data('product-id'); // Get product ID from data attribute

        if (confirm("Are you sure you want to delete this item?")) {
            $.ajax({
                url: '/Cart/DeleteCartItem',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(productId),
                success: function (response) {
                    if (response.success) {
                        alert(response.message);
                        $(`.delete-cart-item[data-product-id="${productId}"]`).closest('.cart-item').remove();
                        updateCartCount(); // Cart count update karo
                    } else {
                        alert(response.message);
                    }
                },
                error: function () {
                    alert("An error occurred while deleting the cart item.");
                }
            });
        }
    });

    // Delete all cart items
    $('.delete-all-cart-items').on('click', function () {
        if (confirm("Are you sure you want to delete all cart items?")) {
            $.ajax({
                url: '/Cart/DeleteAllCartItems',
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        alert(response.message);
                        $('.cart-item').remove();
                        updateCartCount(); // Cart count update karo
                    } else {
                        alert(response.message);
                    }
                },
                error: function () {
                    alert("An error occurred while deleting all cart items.");
                }
            });
        }
    });
    // Address ko save krna ur us ki jaga dosra component render krna
    //$("#addressForm").submit(function (event) {
    //    event.preventDefault();
    //    $.ajax({
    //        url: '/Order/CreateAddress',
    //        type: 'POST',
    //        data: $(this).serialize(),
    //        success: function (result) {
    //            $("#shippingAddressSection").html(result); // ✅ Address Section Update Hoga
    //            $("#addressForm").hide(); // ✅ Form Hide Ho Jayega
    //        }
    //    });
    //});
    $(document).on("submit", "#addressForm", function (e) {
        e.preventDefault(); // Default form submission roko

        var form = $(this);
        var formData = form.serialize();

        $.ajax({
            url: form.attr("action"), // ✅ Ensure correct form action is used
            type: "POST",
            data: formData,
            success: function (response) {
                if (response.redirectToLogin) {
                    window.location.href = "/Account/Login"; // Agar login zaroori hai
                    return;
                }

                $("#shippingAddressSection").html(response); // ✅ Address form replace with Shipping Address
            },
            error: function (xhr) {
                alert("Failed to save address.");
            }
        });
    });


    //  JavaScript (Cart Badge Update)
    updateCartCount();
    function updateCartCount() {
        $.ajax({
            url: '/Cart/GetCartItemCount',
            type: 'GET',
            success: function (count) {
                $(".badge").text(count); // Cart badge update karega
            },
            error: function () {
                console.log("Cart count update failed!");
            }
        });
    }
});