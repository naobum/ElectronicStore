const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5190/api/v1';

const fetchJson = async (url, options = {}) => {
  const response = await fetch(url, options);
  if (!response.ok) {
    const text = await response.text();
    throw new Error(text || `Request failed: ${response.status}`);
  }
  if (response.status === 204) return null;
  return response.json();
};

export const productApi = {
  getAll: async () => fetchJson(`${API_BASE_URL}/products`),
  getById: async (id) => fetchJson(`${API_BASE_URL}/products/${id}`),
  create: async (product) => fetchJson(`${API_BASE_URL}/products`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(product),
  }),
  update: async (id, product) => fetchJson(`${API_BASE_URL}/products/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(product),
  }),
  delete: async (id) => fetchJson(`${API_BASE_URL}/products/${id}`, { method: 'DELETE' }),
};

export const brandApi = {
  getAll: async () => fetchJson(`${API_BASE_URL}/brands`),
  getById: async (id) => fetchJson(`${API_BASE_URL}/brands/${id}`),
  create: async (brand) => fetchJson(`${API_BASE_URL}/brands`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(brand),
  }),
  update: async (id, brand) => fetchJson(`${API_BASE_URL}/brands/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(brand),
  }),
  delete: async (id) => fetchJson(`${API_BASE_URL}/brands/${id}`, { method: 'DELETE' }),
};

export const userApi = {
  getAll: async () => fetchJson(`${API_BASE_URL}/users`),
  getById: async (id) => fetchJson(`${API_BASE_URL}/users/${id}`),
  create: async (user) => fetchJson(`${API_BASE_URL}/users`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(user),
  }),
  update: async (id, user) => fetchJson(`${API_BASE_URL}/users/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(user),
  }),
  delete: async (id) => fetchJson(`${API_BASE_URL}/users/${id}`, { method: 'DELETE' }),
};

export const reviewApi = {
  getAll: async () => fetchJson(`${API_BASE_URL}/reviews`),
  create: async (review) => fetchJson(`${API_BASE_URL}/reviews`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(review),
  }),
  delete: async (id) => fetchJson(`${API_BASE_URL}/reviews/${id}`, { method: 'DELETE' }),
};
