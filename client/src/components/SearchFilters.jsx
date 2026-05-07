import '../styles/SearchFilters.css';

export default function SearchFilters({
  searchTerm,
  onSearchChange,
  brands,
  selectedBrand,
  onBrandChange,
  minRating,
  onRatingChange,
}) {
  return (
    <div className="search-filters">
      <div className="filter-group">
        <label htmlFor="search">Поиск:</label>
        <input
          id="search"
          type="text"
          placeholder="Поиск по названию..."
          value={searchTerm}
          onChange={(e) => onSearchChange(e.target.value)}
        />
      </div>

      <div className="filter-group">
        <label htmlFor="brand">Бренд:</label>
        <select
          id="brand"
          value={selectedBrand || ''}
          onChange={(e) => onBrandChange(e.target.value || null)}
        >
          <option value="">Все бренды</option>
          {brands.map((brand) => (
            <option key={brand.id} value={brand.id}>
              {brand.name}
            </option>
          ))}
        </select>
      </div>

      <div className="filter-group">
        <label htmlFor="rating">Минимальный рейтинг:</label>
        <div className="rating-input">
          <input
            id="rating"
            type="range"
            min="0"
            max="5"
            step="0.1"
            value={minRating}
            onChange={(e) => onRatingChange(parseFloat(e.target.value))}
          />
          <span>{minRating.toFixed(1)} ⭐</span>
        </div>
      </div>
    </div>
  );
}
