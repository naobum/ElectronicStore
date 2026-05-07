import { useCart } from '../context/CartContext';
import '../styles/Cart.css';

export default function Cart() {
  const { cartItems, removeFromCart, updateQuantity, clearCart, getTotalPrice } = useCart();

  const handleCheckout = () => {
    if (cartItems.length === 0) {
      alert('Корзина пуста');
      return;
    }
    alert(`Заказ на сумму ${getTotalPrice().toFixed(2)} успешно создан!`);
    clearCart();
  };

  return (
    <div className="cart-container">
      <h1>Корзина</h1>

      {cartItems.length === 0 ? (
        <p className="empty-cart">Корзина пуста</p>
      ) : (
        <>
          <div className="cart-items">
            {cartItems.map((item) => (
              <div key={item.id} className="cart-item">
                <div className="item-info">
                  <h3>{item.name}</h3>
                  <p>Бренд: {item.brand?.name || 'Неизвестно'}</p>
                  <p>Рейтинг: {item.rating?.toFixed(1) || 'N/A'} ⭐</p>
                  <p className="item-price">Цена: {item.price || 0} ₽</p>
                </div>

                <div className="item-quantity">
                  <button
                    onClick={() =>
                      updateQuantity(item.id, item.quantity - 1)
                    }
                  >
                    -
                  </button>
                  <input
                    type="number"
                    min="1"
                    value={item.quantity}
                    onChange={(e) =>
                      updateQuantity(
                        item.id,
                        parseInt(e.target.value) || 1
                      )
                    }
                  />
                  <button
                    onClick={() =>
                      updateQuantity(item.id, item.quantity + 1)
                    }
                  >
                    +
                  </button>
                </div>

                <div className="item-total">
                  {((item.price || 0) * item.quantity).toFixed(2)} ₽
                </div>

                <button
                  className="remove-btn"
                  onClick={() => removeFromCart(item.id)}
                >
                  Удалить
                </button>
              </div>
            ))}
          </div>

          <div className="cart-summary">
            <div className="total">
              Итого: <strong>{getTotalPrice().toFixed(2)} ₽</strong>
            </div>
            <button className="checkout-btn" onClick={handleCheckout}>
              Оформить заказ
            </button>
            <button className="clear-btn" onClick={clearCart}>
              Очистить корзину
            </button>
          </div>
        </>
      )}
    </div>
  );
}
