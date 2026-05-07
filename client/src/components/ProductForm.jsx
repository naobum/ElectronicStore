import { useState, useEffect } from 'react';
import '../styles/ProductForm.css';

export default function ProductForm({
  brands,
  onSave,
  editingItem,
  onCancel,
}) {
  const [formData, setFormData] = useState({
    name: '',
    brandId: '',
    rating: 0,
    description: '',
  });

  useEffect(() => {
    if (editingItem) {
      setFormData(editingItem);
    } else {
      setFormData({
        name: '',
        brandId: '',
        rating: 0,
        description: '',
      });
    }
  }, [editingItem]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]:
        name === 'rating' || name === 'brandId' ? parseFloat(value) : value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave(formData);
    setFormData({
      name: '',
      brandId: '',
      rating: 0,
      description: '',
    });
  };

  return (
    <form className="product-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="name">Название товара:</label>
        <input
          id="name"
          type="text"
          name="name"
          value={formData.name}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="brandId">Бренд:</label>
        <select
          id="brandId"
          name="brandId"
          value={formData.brandId}
          onChange={handleChange}
          required
        >
          <option value="">Выберите бренд</option>
          {brands.map((brand) => (
            <option key={brand.id} value={brand.id}>
              {brand.name}
            </option>
          ))}
        </select>
      </div>

      <div className="form-group">
        <label htmlFor="rating">Рейтинг:</label>
        <input
          id="rating"
          type="number"
          name="rating"
          min="0"
          max="5"
          step="0.1"
          value={formData.rating}
          onChange={handleChange}
        />
      </div>

      <div className="form-group">
        <label htmlFor="description">Описание:</label>
        <textarea
          id="description"
          name="description"
          value={formData.description}
          onChange={handleChange}
        />
      </div>

      <div className="form-actions">
        <button type="submit">{editingItem ? 'Обновить' : 'Добавить'}</button>
        {editingItem && (
          <button type="button" onClick={onCancel}>
            Отмена
          </button>
        )}
      </div>
    </form>
  );
}
