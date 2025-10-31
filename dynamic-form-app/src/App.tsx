import React from 'react';
import ProductConfigForm from './ProductConfigForm';
import productSchema from './productSchemaAli.json'; // Import your schema
import Ajv from 'ajv/dist/2020';
import addFormats from 'ajv-formats';

const ajv = new Ajv({ strict: false, allErrors: true, allowDate: true });
addFormats(ajv);

const product = {
    payoutStartDate: '2026-01-01',
    payoutPeriod: 64,
};

const validate = ajv.compile(productSchema);
const isValid = validate(product);

console.log('JsonSchema', productSchema);
console.log('p', product);
console.log('isValid', isValid);

const App = () => {
    // const handleFormSubmit = (formData: HTMLFormElement) => {
    //     console.log('Form Data:', formData);
    // };

    // return (
    //     <div className='container mt-5'>
    //         <ProductConfigForm zSchema={zodSchema} jSchema={productSchema} onSubmit={handleFormSubmit} />
    //     </div>
    // );

    return <div className='container mt-5'>Hello Ajv World!</div>;
};

export default App;
