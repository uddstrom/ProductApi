import React, { useState } from 'react';
import { Form, Button, Row, Col } from 'react-bootstrap';

interface IProductConfigFormProps {
    zSchema: ZodType;
    jSchema: JSONSchema;
    onSubmit: (formData: HTMLFormElement) => void;
}

const ProductConfigForm: React.FC<IProductConfigFormProps> = ({ zSchema, jSchema, onSubmit }) => {
    const [formData, setFormData] = useState<HTMLFormElement>({} as HTMLFormElement);
    // const [formErrors, setFormErrors] = useState({});

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({
            ...formData,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleValidation = () => {
        const result = zSchema.safeParse(formData);
        console.log(result);
        if (!result.success) {
            result.error; // ZodError instance
        } else {
            result.data; // { username: string; xp: number }
        }
        return result.success;
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (handleValidation()) {
            onSubmit(formData);
        }
    };

    const renderField = (parameter: object) => {
        switch (parameter.type) {
            default:
                // Handle other HTML5 input types like date, email, etc.
                return (
                    <Form.Control
                        type={parameter.type}
                        name={parameter.name}
                        placeholder={parameter.placeholder}
                        onChange={handleChange}
                        required={parameter.required}
                        value={formData[parameter.name] || ''}
                    />
                );
        }
    };

    return (
        <Form onSubmit={handleSubmit}>
            <h3>{jSchema.title}</h3>
            <Row>
                {Object.entries(jSchema.properties).map(
                    ([parameterName, parameter]: [string, object], fieldIndex: number): void => {
                        return (
                            <Col md={6} sm={12} key={fieldIndex}>
                                <Form.Group className='mb-3'>
                                    <Form.Label>{parameterName}</Form.Label>
                                    {renderField(parameter)}
                                    {/* {formErrors[field.name] && ( */}
                                    {/*     <div className="text-danger"> */}
                                    {/*         {formErrors[field.name]} */}
                                    {/*     </div> */}
                                    {/* )} */}
                                </Form.Group>
                            </Col>
                        );
                    }
                )}
            </Row>
            <Button variant='primary' type='submit'>
                Submit
            </Button>
        </Form>
    );
};

export default ProductConfigForm;
