import { useEffect, useState } from 'react';
import '../styles/BrandForm.css';

export default function BrandForm({ editingItem, onSave, onCancel }) {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  useEffect(() => {
    if (editingItem) {
      setName(editingItem.name || '');
      setDescription(editingItem.description || '');
    } else {
      setName('');
      setDescription('');
    }
  }, [editingItem]);

  const handleSubmit = (event) => {
    event.preventDefault();
    onSave({ name, description });
  };

  return (
    <form className="brand-form" onSubmit={handleSubmit}>
      <div className="form-row">
        <label>
          Название
          <input value={name} onChange={(e) => setName(e.target.value)} required />
        </label>
      </div>
      <label>
        Описание
        <textarea value={description} onChange={(e) => setDescription(e.target.value)} />
      </label>
      <div className="form-actions">
        <button type="submit" className="primary-btn">
          {editingItem ? 'Сохранить бренд' : 'Добавить бренд'}
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
