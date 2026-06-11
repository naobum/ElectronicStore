import { useMemo, useState } from 'react';
import { useCart } from '../context/CartContext.jsx';
import '../styles/Cart.css';

export default function Cart() {
  const { cartItems, updateQuantity, removeFromCart, clearCart, totalPrice, buyCart } = useCart();
  const [buying, setBuying] = useState(false);

  const hasItems = cartItems.length > 0;

  const totalCount = useMemo(
    () => cartItems.reduce((sum, item) => sum + item.quantity, 0),
    [cartItems]
  );

  const handleBuy = async () => {
    try {
      setBuying(true);
      await buyCart(1);
      alert('Покупка оформлена успешно');
    } catch (err) {
      alert('Ошибка покупки: ' + err.message);
    } finally {
      setBuying(false);
    }
  };

  return (
    <div className="page-shell">
      <h1>Корзина</h1>
      {!hasItems ? (
        <div className="loading">Корзина пуста. Добавьте товары на страницу каталога.</div>
      ) : (
        <div className="cart-shell">
          <div className="cart-list">
            {cartItems.map((item) => (
              <div key={item.id} className="cart-item">
                <div>
                  <div className="cart-item-title">{item.name}</div>
                  <div className="cart-item-meta">Цена: {item.price} ₽ · Остаток: {item.amount}</div>
                </div>
                <div className="cart-item-controls">
                  <input
                    type="number"
                    min="1"
                    max={item.amount || 999}
                    value={item.quantity}
                    onChange={(e) => updateQuantity(item.id, Number(e.target.value))}
                  />
                  <button className="delete-btn" onClick={() => removeFromCart(item.id)}>
                    Удалить
                  </button>
                </div>
              </div>
            ))}
          </div>
          <div className="cart-summary">
            <div className="cart-summary-row">Всего товаров: {totalCount}</div>
            <div className="cart-summary-row">Итого: {totalPrice.toFixed(2)} ₽</div>
            <div className="cart-summary-row">Аккаунт: user (демо)</div>
            <div className="cart-summary-row">Баланс не учитывается при покупке.</div>
            <button className="primary-btn" onClick={handleBuy} disabled={buying}>
              {buying ? 'Оформляем...' : 'Купить'}
            </button>
            <button className="secondary-btn" onClick={clearCart}>
              Очистить корзину
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
