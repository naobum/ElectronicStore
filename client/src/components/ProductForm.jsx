import { useEffect, useState } from 'react';
import '../styles/ProductForm.css';

export default function ProductForm({ brands, editingItem, onSave, onCancel }) {
  const [name, setName] = useState('');
  const [brandId, setBrandId] = useState('');
  const [price, setPrice] = useState('');
  const [amount, setAmount] = useState('');
  const [rating, setRating] = useState('');
  const [description, setDescription] = useState('');

  useEffect(() => {
    if (editingItem) {
      setName(editingItem.name || '');
      setBrandId(editingItem.brandId?.toString() || '');
      setPrice(editingItem.price?.toString() || '');
      setAmount(editingItem.amount?.toString() || '');
      setRating(editingItem.rating?.toString() || '0');
      setDescription(editingItem.description || '');
    } else {
      setName('');
      setBrandId('');
      setPrice('');
      setAmount('');
      setRating('0');
      setDescription('');
    }
  }, [editingItem]);

  const handleSubmit = (event) => {
    event.preventDefault();

    const trimmedName = name.trim();
    const parsedBrandId = Number(brandId);
    const parsedPrice = Number(price);
    const parsedAmount = Number(amount);
    console.log('=== ДИАГНОСТИКА ===');
    console.log('brandId (state):', brandId);
    console.log('brandId type:', typeof brandId);
    console.log('parsedBrandId:', parsedBrandId);
    console.log('brands array:', brands);
    console.log('Available brand ids:', brands.map(b => ({ id: b.id, name: b.name })));
    if (!trimmedName) {
      alert('Введите название товара');
      return;
    }

    if (!brandId || isNaN(parsedBrandId) ||parsedBrandId <= 0) {
      alert('Выберите бренд товара');
      console.log("brandId:", brandId, "parsedBrandId:", parsedBrandId);
      return;
    }

    if (!Number.isFinite(parsedPrice) || parsedPrice < 0) {
      alert('Введите корректную цену');
      return;
    }

    if (!Number.isFinite(parsedAmount) || parsedAmount < 0) {
      alert('Введите корректное количество');
      return;
    }

    const parsedRating = Number(rating);
    if (!Number.isFinite(parsedRating) || parsedRating < 0 || parsedRating > 5) {
      alert('Рейтинг должен быть от 0 до 5');
      return;
    }

    onSave({
      name: trimmedName,
      brandId: parsedBrandId,
      price: parsedPrice,
      amount: Math.trunc(parsedAmount),
      rating: parsedRating,
      description: description.trim(),
    });
  };

  return (
    <form className="product-form" onSubmit={handleSubmit}>
      <div className="form-row">
        <label>
          Название
          <input value={name} onChange={(e) => setName(e.target.value)} required />
        </label>
        <label>
          Бренд
          <select value={brandId} onChange={(e) => setBrandId(e.target.value)} required>
            <option value="">Выберите бренд</option>
            {brands.map((brand) => (
              <option key={brand.id} value={String(brand.id)}>{brand.name}</option>
            ))}
          </select>
        </label>
      </div>
      <div className="form-row">
        <label>
          Цена
          <input type="number" min="0" step="0.01" value={price} onChange={(e) => setPrice(e.target.value)} required />
        </label>
        <label>
          Количество
          <input type="number" min="1" value={amount} onChange={(e) => setAmount(e.target.value)} required />
        </label>
        <label>
          Рейтинг
          <input type="number" min="0" max="5" step="0.1" value={rating} onChange={(e) => setRating(e.target.value)} required />
        </label>
      </div>
      <label>
        Описание
        <textarea value={description} onChange={(e) => setDescription(e.target.value)} />
      </label>
      <div className="form-actions">
        <button type="submit" className="primary-btn">
          {editingItem ? 'Сохранить товар' : 'Добавить товар'}
        </button>
        {editingItem && (
          <button type="button" className="secondary-btn" onClick={onCancel}>
            Отмена
          </button>
        )}
      </div>
    </form>
  );
}
