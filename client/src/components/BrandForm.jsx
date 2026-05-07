import { useState, useEffect } from 'react';
import '../styles/BrandForm.css';

export default function BrandForm({
  onSave,
  editingItem,
  onCancel,
}) {
  const [formData, setFormData] = useState({
    name: '',
    description: '',
  });

  useEffect(() => {
    if (editingItem) {
      setFormData(editingItem);
    } else {
      setFormData({
        name: '',
        description: '',
      });
    }
  }, [editingItem]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave(formData);
    setFormData({
      name: '',
      description: '',
    });
  };

  return (
    <form className="brand-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="name">Название бренда:</label>
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
