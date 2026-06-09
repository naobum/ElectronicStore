import { useEffect, useState } from 'react';
import '../styles/ProductForm.css';

export default function ProductForm({ brands, editingItem, onSave, onCancel }) {
  const [name, setName] = useState('');
  const [brandId, setBrandId] = useState('');
  const [price, setPrice] = useState('');
  const [amount, setAmount] = useState('');
  const [description, setDescription] = useState('');

  useEffect(() => {
    if (editingItem) {
      setName(editingItem.name || '');
      setBrandId(editingItem.brandId?.toString() || '');
      setPrice(editingItem.price?.toString() || '');
      setAmount(editingItem.amount?.toString() || '');
      setDescription(editingItem.description || '');
    } else {
      setName('');
      setBrandId('');
      setPrice('');
      setAmount('');
      setDescription('');
    }
  }, [editingItem]);

  const handleSubmit = (event) => {
    event.preventDefault();
    onSave({
      name,
      brandId: Number(brandId),
      price: Number(price),
      amount: Number(amount),
      description,
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
              <option key={brand.id} value={brand.id}>{brand.name}</option>
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
